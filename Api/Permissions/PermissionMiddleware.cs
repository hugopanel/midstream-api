using System.Security.Claims;
using Application.Common.Interfaces.Persistence;

namespace Api.Permissions;

public class PermissionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRolePermissionsCache _rolePermissionsCache;
    
    public PermissionMiddleware(RequestDelegate next, IRolePermissionsCache rolePermissionsCache)
    {
        _next = next;
        _rolePermissionsCache = rolePermissionsCache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get the endpoint
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            var requiresPermissionAttributes = endpoint.Metadata.GetOrderedMetadata<RequiresPermissionAttribute>();

            if (requiresPermissionAttributes.Any())
            {
                Console.WriteLine("REQUIRES PERMISSIONS ATTRIBUTE");
                
                // Get the user roles
                var user = context.User;
                if (user.Identity?.IsAuthenticated != true)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                var userPermissions = new HashSet<string>();

                foreach (var roleIdString in userRoles)
                {
                    var permissions = await _rolePermissionsCache.GetPermissionsForRoleAsync(Guid.Parse(roleIdString));
                    foreach (var permission in permissions)
                    {
                        userPermissions.Add(permission.Code);
                    }
                }

                if (requiresPermissionAttributes.Any(attribute => !userPermissions.Contains(attribute.PermissionCode)))
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden");
                    return;
                }
            }
        }
        
        // Call the next delegate/middleware in the pipeline
        await _next(context);
    }
}
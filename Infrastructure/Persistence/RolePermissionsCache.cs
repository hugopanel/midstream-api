using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public class RolePermissionsCache : IRolePermissionsCache
{
    // private static IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(1);
    private readonly IServiceProvider _serviceProvider;

    public RolePermissionsCache(IMemoryCache cache, IServiceProvider serviceProvider)
    {
        _cache = cache;
        _serviceProvider = serviceProvider;
    }
    
    public async Task<IEnumerable<Permission>> GetPermissionsForRoleAsync(Guid roleId)
    {
        if (!_cache.TryGetValue(roleId, out IEnumerable<Permission>? permissions))
        {
            using var scope = _serviceProvider.CreateScope();
            var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
            permissions = roleRepository.GetRoleFromId(roleId)?.Permissions;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(_cacheDuration);

            _cache.Set(roleId, permissions, cacheEntryOptions);
        }

        return permissions ?? new List<Permission>();
    }

    public void ClearCacheForRole(Guid roleId)
    {
        _cache.Remove(roleId);
    }
}
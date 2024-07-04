namespace Api.Permissions;

// [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class RequiresPermissionAttribute : Attribute
{
    public string PermissionCode { get; }
    
    public RequiresPermissionAttribute(string permissionCode)
    {
        PermissionCode = permissionCode;
    }
}
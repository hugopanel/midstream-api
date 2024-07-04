using System.Reflection;
using Domain.Interfaces;

namespace Domain.Permissions;

public static class PermissionMapper
{
    public static List<Permission> Permissions { get; } = new List<Permission>();
    private static List<Type> RegisteredPermissionClasses { get; } = new List<Type>();

    public static void RegisterPermissionClass(Type permissionClass)
    {
        if (RegisteredPermissionClasses.Contains(permissionClass))
            return;
        
        if (!permissionClass.IsClass)
            throw new ArgumentException("Provided type is not a class: ", nameof(permissionClass));

        var members = permissionClass.GetMembers(BindingFlags.Static | BindingFlags.Public)
            .Where(p => !p.Name.EndsWith("Code"));


        var memberInfos = members.ToList();
        if (memberInfos.Count != 0)
        {
            RegisteredPermissionClasses.Add(permissionClass);
            
            foreach (var member in memberInfos)
            {
                Console.WriteLine(member.Name);
                // Check if the member is a permission
                if (member is FieldInfo fieldInfo)
                {
                    if (fieldInfo.FieldType.BaseType == typeof(Permission))
                    {
                        var permission = (Permission)fieldInfo.GetValue(null);
                        Permissions.Add(permission);
                    }
                }
            }
        }
    }
}
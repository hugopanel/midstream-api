using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class RoleRepository : IRoleRepository
{
    private UserDbContext _userDbContext;

    public RoleRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public void AddPermissionToRole(Role role, Permission permission)
    {
        _userDbContext.Role.Attach(role);
        if (!role.Permissions.Contains(permission)) role.Permissions.Add(permission);
        _userDbContext.SaveChanges();
    }

    public void RemovePermissionFromRole(Role role, Permission permission)
    {
        _userDbContext.Role.Attach(role);
        if (role.Permissions.Contains(permission)) role.Permissions.Remove(permission);
        _userDbContext.SaveChanges();
    }

    public Role? GetRoleFromId(Guid roleId)
    {
        var role = _userDbContext.Role.FirstOrDefault(r => r.Id == roleId);
        return role;
    }

    public Role[] GetRolesFromName(string roleName)
    {
        return _userDbContext.Role.Where(r => r.Name == roleName).ToArray();
    }

    public void Add(Role role)
    {
        _userDbContext.Role.Add(role);
        _userDbContext.SaveChanges();
    }

    public void Save(Role role)
    {
        _userDbContext.Role.Update(role);
        _userDbContext.SaveChanges();
    }

    public void Attach(Role role)
    {
        _userDbContext.Role.Attach(role);
    }
}
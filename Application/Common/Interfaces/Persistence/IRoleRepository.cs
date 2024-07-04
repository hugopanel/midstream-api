using Domain.Entities;
using Domain.Interfaces;

namespace Application.Common.Interfaces.Persistence;

public interface IRoleRepository
{
    public void AddPermissionToRole(Role role, Permission permission);
    public void RemovePermissionFromRole(Role role, Permission permission);
    public Role? GetRoleFromId(Guid roleId);
    public Role[] GetRolesFromName(string roleName);
    
    public void Add(Role role);
    public void Save(Role role);
    public void Attach(Role role);
}
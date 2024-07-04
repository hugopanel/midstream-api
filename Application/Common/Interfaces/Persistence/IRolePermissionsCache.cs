using Domain.Entities;
using Domain.Interfaces;

namespace Application.Common.Interfaces.Persistence;

public interface IRolePermissionsCache
{
    public Task<IEnumerable<Permission>> GetPermissionsForRoleAsync(Guid roleId);
    public void ClearCacheForRole(Guid roleId);
}
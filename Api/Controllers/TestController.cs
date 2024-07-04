using Api.Permissions;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Permissions;
using Domain.Permissions.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Test")]
public class TestController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRolePermissionsCache _cache;

    public TestController(IRoleRepository roleRepository, IRolePermissionsCache cache)
    {
        _roleRepository = roleRepository;
        _cache = cache;
    }

    [HttpPost("AddRole")]
    [Authorize]
    public async Task<IActionResult> AddRole(string name)
    {
        Role role = new Role()
        {
            Name = name,
            Permissions =  new List<Permission>()
            
        };

        _roleRepository.Add(role);

        return Ok(role);
    }

    [HttpGet]
    public async Task<IActionResult> GetRolesFromName(string roleName)
    {
        return Ok(_roleRepository.GetRolesFromName(roleName));
    }
    
    [HttpPost("AddPermission")]
    [Authorize]
    public async Task<IActionResult> AddPermission(string roleId, string permissionCode)
    {
        var role = _roleRepository.GetRoleFromId(Guid.Parse(roleId));
        if (role == null) return NotFound("Role not found.");
        
        Console.WriteLine("Adding permission " + permissionCode + ".");
        var permission = PermissionMapper.Permissions.FirstOrDefault(p => p.Code == permissionCode);
        if (permission == null)
        {
            Console.WriteLine("No permission found!");
            return NotFound("Permission not found in permissions mapper. Has it been registered?");
        }
        
        role.Permissions.Add(permission);
        
        _roleRepository.Save(role);
        
        _cache.ClearCacheForRole(Guid.Parse(roleId));
        
        return Ok();
    }

    [HttpGet("GetPermissionsFromRole")]
    public async Task<IActionResult> GetPermissionsFromRole(string roleId)
    {
        var role = _roleRepository.GetRoleFromId(Guid.Parse(roleId));
        if (role == null) return NotFound("Role not found.");
        
        return Ok(role.Permissions);
    }

    [HttpGet("GetPermissionsFromMapper")]
    public async Task<IActionResult> GetPermissionsFromMapper()
    {
        return Ok(PermissionMapper.Permissions);
    }

    [HttpGet("GetAdminPermissions")]
    public async Task<IActionResult> GetAdminPermissions()
    {
        string[] permissions = 
        {
            AdministrationPermissions.CreateTeamCode,
            AdministrationPermissions.UpdateTeamCode,
            AdministrationPermissions.DeleteTeamCode,
            AdministrationPermissions.CreateProjectCode,
            AdministrationPermissions.UpdateProjectCode,
            AdministrationPermissions.DeleteProjectCode,
            AdministrationPermissions.CreateRoleCode,
            AdministrationPermissions.UpdateRoleCode,
            AdministrationPermissions.DeleteRoleCode,
            AdministrationPermissions.DeleteUserCode
        };

        return Ok(permissions);
    }

    [Authorize]
    [HttpGet("TestPermission")]
    [RequiresPermission(AdministrationPermissions.CreateTeamCode)]
    public async Task<IActionResult> TestPermission()
    {
        return Ok("This works!");
    }
}
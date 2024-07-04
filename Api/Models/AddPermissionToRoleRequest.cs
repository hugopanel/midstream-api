using Domain.Entities;
using Domain.Interfaces;

namespace Api.Models;

public record AddPermissionToRoleRequest(
    Role role, 
    Permission permission
);
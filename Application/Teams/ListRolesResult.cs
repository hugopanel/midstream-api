using Domain.Entities;

namespace Application.Teams;

public record ListRolesResult(
    List<Role> Roles
);
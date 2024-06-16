namespace Api.Models;

public record CreateTeamRequest(string Name, string ProjectId, List<UserRoles> Users);

public record UserRoles(string UserId, List<string> RolesId);


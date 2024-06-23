namespace Api.Models;

public record CreateTeamRequest(string Name, List<UserToAdd> memberstoadd);

public record UserToAdd(string userId, List<string> rolesId);


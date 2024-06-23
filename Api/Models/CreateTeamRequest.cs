namespace Api.Models;

public record CreateTeamRequest(string ProjectId, List<UserToAdd> memberstoadd);

public record UserToAdd(string userId, List<string> rolesId);


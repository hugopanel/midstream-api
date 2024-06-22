namespace Api.Models;

public record CreateMemberRequest(string UserId, string TeamId, List<string> RolesId);
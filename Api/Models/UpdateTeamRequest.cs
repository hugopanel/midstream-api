using Domain.Entities;

namespace Api.Models;

public record UpdateTeamRequest(string teamId, string name, List<MemberToAdd> memberstoadd, List<MemberRoleToAdd> membersroletoadd);

public record MemberToAdd(string userId, List<string> rolesId);

public record MemberRoleToAdd(string memberId, string roleId);
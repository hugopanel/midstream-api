using MediatR;

namespace Application.Teams.Commands;

public record UpdateTeamCommand(string teamId, string name, List<MemberToAdd> memberstoadd, List<MemberRoleToAdd> membersroletoadd) : IRequest<StringResult>;

public record MemberToAdd(string userId, List<string> rolesId);

public record MemberRoleToAdd(string memberId, string roleId);
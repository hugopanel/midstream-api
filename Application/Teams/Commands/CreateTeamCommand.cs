using MediatR;

namespace Application.Teams.Commands;

public record CreateTeamCommand(string ProjectId, List<UserToAdd> memberstoadd) : IRequest<TeamResult>;

public record UserToAdd(string userId, List<string> rolesId);
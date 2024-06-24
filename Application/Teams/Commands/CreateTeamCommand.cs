using MediatR;

namespace Application.Teams.Commands;

public record CreateTeamCommand(string Name, List<UserToAdd> memberstoadd) : IRequest<TeamResult>;

public record UserToAdd(string userId, List<string> rolesId);
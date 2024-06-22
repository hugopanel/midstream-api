using MediatR;

namespace Application.Teams.Commands;

public record CreateTeamCommand(string Name, string ProjectId) : IRequest<TeamResult>;
using MediatR;

namespace Application.Teams.Commands;

public record DeleteTeamCommand(string teamId) : IRequest<StringResult>;
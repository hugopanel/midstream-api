using MediatR;

namespace Application.Teams.Queries;

public record GetTeamNameByIdQuery(string teamId) : IRequest<StringResult>;
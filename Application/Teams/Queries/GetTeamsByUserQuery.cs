using MediatR;

namespace Application.Teams.Queries;

public record GetTeamsByUserQuery(string userId) : IRequest<ListTeamsResult>;
using MediatR;

namespace Application.Teams.Queries;

public record GetTeamsQuery() : IRequest<ListTeamsResult>;
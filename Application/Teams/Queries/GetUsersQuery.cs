using MediatR;

namespace Application.Teams.Queries;

public record GetUsersQuery() : IRequest<ListUsersToDisplayResult>;
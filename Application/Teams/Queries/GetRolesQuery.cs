using MediatR;

namespace Application.Teams.Queries;

public record GetRolesQuery() : IRequest<ListRolesResult>;
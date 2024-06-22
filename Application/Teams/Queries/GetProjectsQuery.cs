using MediatR;

namespace Application.Teams.Queries;

public record GetProjectsQuery() : IRequest<ListProjectsResult>;
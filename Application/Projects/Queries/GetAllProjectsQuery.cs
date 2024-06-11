using MediatR;
namespace Application.Projects.Queries;

public record GetAllProjectsQuery() : IRequest<GetProjectsResult>;
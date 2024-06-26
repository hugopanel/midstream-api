using MediatR;
namespace Application.Projects.Queries;


public record GetProjectsQuery() : IRequest<GetProjectsResult>;
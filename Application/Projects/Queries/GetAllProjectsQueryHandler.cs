
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Projects.Queries;

public class GetAllProjectsQueryHandler(IProjectRepository projectRepository)
    :IRequestHandler<GetAllProjectsQuery, GetProjectsResult>
{
    private IProjectRepository _projectRepository = projectRepository;

    public async Task<GetProjectsResult> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = _projectRepository.GetAllProjects();
                
        return new GetProjectsResult(projects);
    }

}

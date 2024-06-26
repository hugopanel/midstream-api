
using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Projects.Queries;

public class GetProjectsQueryHandler(IProjectRepository projectRepository)
    :IRequestHandler<GetProjectsQuery, GetProjectsResult>
{
    private IProjectRepository _projectRepository = projectRepository;

    public async Task<GetProjectsResult> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = _projectRepository.GetProjects();
                
        return new GetProjectsResult(projects);
    }

}

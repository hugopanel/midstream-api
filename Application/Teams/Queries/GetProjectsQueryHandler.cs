using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public class GetProjectsQueryHandler(IProjectRepository projectRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetProjectsQuery, ListProjectsResult>
{
    private IProjectRepository _projectRepository = projectRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListProjectsResult> Handle(GetProjectsQuery query, CancellationToken cancellationToken)
    {
        var projects = _projectRepository.GetAllProjects();

        // Return all projects
        return new ListProjectsResult(projects);
    }
}
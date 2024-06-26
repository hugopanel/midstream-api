using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Teams;
using MediatR;

namespace Application.Teams.Queries;

public class GetProjectsByUserQueryHandler(IMemberRepository memberRepository, ITeamRepository teamRepository, IProjectRepository projectRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetProjectsByUserQuery, ListProjectsResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private ITeamRepository _TeamRepository = teamRepository;
    private IProjectRepository _ProjectRepository = projectRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListProjectsResult> Handle(GetProjectsByUserQuery query, CancellationToken cancellationToken)
    {
        var teamsId = _MemberRepository.GetTeamsIdByUserId(query.userId);

        var projects = new List<Project>();

        for(var i = 0; i < teamsId.Count; i++)
        {
            string projectId  = _TeamRepository.GetProjectIdByTeamId(teamsId[i].ToString());
            
            var project = _ProjectRepository.GetProjectById(projectId);

            projects.Add(project);
        }

        // Return projects
        return new ListProjectsResult(projects);
    }
}
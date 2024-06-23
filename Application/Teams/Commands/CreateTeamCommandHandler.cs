using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class CreateTeamCommandHandler(ITeamRepository teamRepository, IProjectRepository projectRepository, IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<CreateTeamCommand, TeamResult>
{
    private ITeamRepository _teamRepository = teamRepository;
    private IProjectRepository _projectRepository = projectRepository;
    private IMemberRepository _memberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<TeamResult> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
    {
        var project = _projectRepository.GetProjectById(command.ProjectId);

        // Create the new team
        var newTeam = new Team
        {
            Id = Guid.NewGuid(),
            Name = project.Name,
            ProjectId =project.Id
        };

        // Add new team
        _teamRepository.Add(newTeam);

        // Add members to the team
        foreach (var membertoadd in command.memberstoadd)
        {
            var newMember = new Member
            {
                Id = Guid.NewGuid(),
                TeamId = newTeam.Id,
                UserId = Guid.Parse(membertoadd.userId)
            };

            _memberRepository.Add(newMember);

            // Add roles to the members
            foreach (var roleId in membertoadd.rolesId)
            {
                var newMemberRole = new MemberRole
                {
                    Id = Guid.NewGuid(),
                    MemberId = newMember.Id,
                    RoleId = Guid.Parse(roleId)
                };
                _memberRepository.AddMemberRole(newMemberRole);
                
            }

        }

        // Return new team
        return new TeamResult(newTeam);
    }
}
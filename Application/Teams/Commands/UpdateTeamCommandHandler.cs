using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class UpdateTeamCommandHandler(ITeamRepository teamRepository, IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<UpdateTeamCommand, StringResult>
{
    private ITeamRepository _teamRepository = teamRepository;
    private IMemberRepository _memberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<StringResult> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
    {
        // Update the new team
        var team = _teamRepository.GetTeamById(command.teamId);

        if (team == null)
        {
            throw new Exception("Team not found");
        }
        else
        {
            // Add members to the team
            foreach (var membertoadd in command.memberstoadd)
            {
                var newMember = new Member
                {
                    Id = Guid.NewGuid(),
                    TeamId = team.Id,
                    UserId = Guid.Parse(membertoadd.userId)
                };

                _memberRepository.Add(newMember);
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

            foreach(var memberroletoadd in command.membersroletoadd)
            {
                var newMemberRole = new MemberRole
                {
                    Id = Guid.NewGuid(),
                    MemberId = Guid.Parse(memberroletoadd.memberId),
                    RoleId = Guid.Parse(memberroletoadd.roleId)
                };
                _memberRepository.AddMemberRole(newMemberRole);
            }

            team.Name = command.name;
            _teamRepository.Save(team); ;
            // Return new team
            return new StringResult("Team updated successfully.");
        }
    }
}
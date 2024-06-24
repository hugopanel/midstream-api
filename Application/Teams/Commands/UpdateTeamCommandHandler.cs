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

            // Find all the distinct ids of the members of the team
            var members = _memberRepository.GetMembersByTeamId(team.Id.ToString());
            var membersId = members.Select(m => m.Id).Distinct().ToList();

            if (membersId.Count > 0)
            {
                foreach (var memberId in membersId)
                {
                    // Remove all the roles of the member
                    var memberRoles = _memberRepository.GetMemberRolesByMemberId(memberId.ToString());
                    foreach (var memberRole in memberRoles)
                    {
                        _memberRepository.DeleteMemberRole(memberRole);
                    }
                }
            }

            if (command.membersroletoadd.Count > 0)
            {
                // Add roles to the members
                foreach (var memberroletoadd in command.membersroletoadd)
                {
                    if (memberroletoadd.memberId != "" && memberroletoadd.roleId != "")
                    {
                        var newMemberRole = new MemberRole
                        {
                            Id = Guid.NewGuid(),
                            MemberId = Guid.Parse(memberroletoadd.memberId),
                            RoleId = Guid.Parse(memberroletoadd.roleId)
                        };
                        _memberRepository.AddMemberRole(newMemberRole);
                    }
                }
            }

            // Add members to the team
            if (command.memberstoadd.Count > 0)
            {
                foreach (var membertoadd in command.memberstoadd)
                {
                    if (membertoadd.userId != "")
                    {
                        if (_memberRepository.GetMemberById(membertoadd.userId) != null)
                        {
                            throw new Exception("Member already exists");
                        }

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

                }
            }

            team.Name = command.name;
            _teamRepository.Save(team); ;
            // Return new team
            return new StringResult("Team updated successfully.");
        }
    }
}
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class DeleteTeamCommandHandler(ITeamRepository teamRepository, IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<DeleteTeamCommand, StringResult>
{
    private ITeamRepository _teamRepository = teamRepository;
    private IMemberRepository _memberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<StringResult> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
    {
        // Delete the new team
        var team = _teamRepository.GetTeamById(command.teamId);

        if (team == null)
        {
            throw new Exception("Team not found");
        }
        else
        {
            // Delete all members of the team
            var members = _memberRepository.GetMembersByTeamId(command.teamId);
            foreach (var member in members)
            {
                var memberRoles = _memberRepository.GetMemberRolesByMemberId(member.Id.ToString());
                foreach (var memberRole in memberRoles)
                {
                    _memberRepository.DeleteMemberRole(memberRole);
                }
                _memberRepository.Delete(member);
            }


            // Add new team
            _teamRepository.Delete(team);

            // Return new team
            return new StringResult("Team deleted successfully.");
        }
    }
}
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Teams;
using MediatR;

namespace Application.Teams.Queries;

public class GetMembersByTeamQueryHandler(IMemberRepository memberRepository, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetMembersByTeamQuery, ListMembersToDisplayResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private IUserRepository _userRepository = userRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListMembersToDisplayResult> Handle(GetMembersByTeamQuery query, CancellationToken cancellationToken)
    {
        var members = _MemberRepository.GetMembersByTeamId(query.TeamId);

        var memberstodisplay = new List<MemberToDisplay>();

        for(var i = 0; i < members.Count; i++)
        {
            var user = _userRepository.GetUserById(members[i].UserId.ToString());
            var rolesId = _MemberRepository.GetRolesByMemberId(members[i].Id.ToString());

            var roles = new List<string>();

            for (var j = 0; j < rolesId.Count; j++)
            {
                var role = _MemberRepository.GetRoleById(rolesId[j]);
                roles.Add(role.Name);
            }

            var memberToDisplay = new MemberToDisplay(members[i].Id.ToString(), user.Username, user.Email, user.Avatar, user.Colour, roles);
            memberstodisplay.Add(memberToDisplay);
        }

        // Return new Member
        return new ListMembersToDisplayResult(memberstodisplay);
    }
}
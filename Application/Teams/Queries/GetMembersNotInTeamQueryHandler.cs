using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Teams;
using MediatR;

namespace Application.Teams.Queries;

public class GetMembersNotInTeamQueryHandler(IMemberRepository memberRepository, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetMembersNotInTeamQuery, ListUsersToDisplayResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private IUserRepository _userRepository = userRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListUsersToDisplayResult> Handle(GetMembersNotInTeamQuery query, CancellationToken cancellationToken)
    {
        var members = _MemberRepository.GetMembersNotInTeam(query.TeamId);

        var users = new List<UserToDisplay>();

        for (var i = 0; i < members.Count; i++)
        {
            var user = _userRepository.GetUserById(members[i].ToString());
            var memberId = "";
            var roles = new List<string>();
            var memberToDisplay = new UserToDisplay(user.Id.ToString(), user.Username, user.Email, user.Avatar, user.Colour, roles);
            users.Add(memberToDisplay);
        }

        // Return users
        return new ListUsersToDisplayResult(users);
    }
}
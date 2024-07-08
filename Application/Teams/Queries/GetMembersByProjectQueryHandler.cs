using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Teams;
using MediatR;

namespace Application.Teams.Queries;

public class GetMembersByProjectQueryHandler(IMemberRepository memberRepository, IUserRepository userRepository, ITeamRepository teamRepository , IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetMembersByProjectQuery, ListMembersToSelectResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private IUserRepository _userRepository = userRepository;
    private ITeamRepository _teamRepository = teamRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListMembersToSelectResult> Handle(GetMembersByProjectQuery query, CancellationToken cancellationToken)
    {

        var team = _teamRepository.GetTeamByProjectId(query.projectId);

        var members = _MemberRepository.GetMembersByTeamId(team.Id.ToString());

        var MembersToSelect = new List<MemberToSelect>();

        for(var i = 0; i < members.Count; i++)
        {
            var user = _userRepository.GetUserById(members[i].UserId.ToString());

            var memberToSelect = new MemberToSelect(user.Id.ToString(), user.Username);
            MembersToSelect.Add(memberToSelect);
        }

        // Return new Member
        return new ListMembersToSelectResult(MembersToSelect);
    }
}
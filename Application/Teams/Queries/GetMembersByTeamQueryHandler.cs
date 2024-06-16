using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public class GetMembersByTeamQueryHandler(IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetMembersByTeamQuery, ListMembersResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListMembersResult> Handle(GetMembersByTeamQuery query, CancellationToken cancellationToken)
    {
        var members = _MemberRepository.GetMembersByTeamId(query.TeamId);

        // Return new Member
        return new ListMembersResult(members);
    }
}
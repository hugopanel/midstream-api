using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public class GetRolesQueryHandler(IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetRolesQuery, ListRolesResult>
{
    private IMemberRepository _memberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListRolesResult> Handle(GetRolesQuery query, CancellationToken cancellationToken)
    {
        var roles = _memberRepository.GetRoles();

        // Return all roles
        return new ListRolesResult(roles);
    }
}
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class CreateMemberRoleCommandHandler(IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<CreateMemberRoleCommand, MemberRoleResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<MemberRoleResult> Handle(CreateMemberRoleCommand command, CancellationToken cancellationToken)
    {
        // Create the new Member
        var newMemberRole = new MemberRole
        {
            Id = Guid.NewGuid(),
            RoleId = Guid.Parse(command.RoleId),
            MemberId = Guid.Parse(command.MemberId)
        };

        // Add new Member
        _MemberRepository.AddMemberRole(newMemberRole);

        // Return new Member
        return new MemberRoleResult(newMemberRole);
    }
}
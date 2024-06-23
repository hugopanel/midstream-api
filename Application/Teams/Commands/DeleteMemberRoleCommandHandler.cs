using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class DeleteMemberRoleCommandHandler(IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<DeleteMemberRoleCommand, StringResult>
{
    private IMemberRepository _memberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<StringResult> Handle(DeleteMemberRoleCommand command, CancellationToken cancellationToken)
    {
        // Delete the new team
        var memberRole = _memberRepository.GetMemberRole(command.memberId, command.roleId);

        if (memberRole == null)
        {
            throw new Exception("MemberRole not found");
        }

        // Add new team
        _memberRepository.DeleteMemberRole(memberRole);

        // Return new team
        return new StringResult("MemberRole deleted successfully.");
    }
}
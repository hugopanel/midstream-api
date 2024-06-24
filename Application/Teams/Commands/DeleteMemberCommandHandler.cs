using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class DeleteMemberCommandHandler(IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<DeleteMemberCommand, StringResult>
{
    private IMemberRepository _memberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<StringResult> Handle(DeleteMemberCommand command, CancellationToken cancellationToken)
    {
        // Delete the new team
        var member = _memberRepository.GetMemberById(command.memberId);

        if (member == null)
        {
            throw new Exception("Member not found");
        }
        else
        {
            var memberRoles = _memberRepository.GetMemberRolesByMemberId(member.Id.ToString());
            foreach (var memberRole in memberRoles)
            {
                _memberRepository.DeleteMemberRole(memberRole);
            }
            _memberRepository.Delete(member);
            
            return new StringResult("Member deleted successfully.");
        }
    }
}
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class CreateMemberCommandHandler(IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<CreateMemberCommand, MemberResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<MemberResult> Handle(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        // Create the new Member
        var newMember = new Member
        {
            Id = Guid.NewGuid(),
            TeamId = Guid.Parse(command.TeamId),
            UserId = Guid.Parse(command.UserId)
        };

        // Add new Member
        _MemberRepository.Add(newMember);

        // Return new Member
        return new MemberResult(newMember);
    }
}
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class CreateRoleCommandHandler(IMemberRepository memberRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<CreateRoleCommand, RoleResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<RoleResult> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        // Create the new Role
        var newRole = new Role
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        // Add new Role
        _MemberRepository.AddRole(newRole);

        // Return new Role
        return new RoleResult(newRole);
    }
}
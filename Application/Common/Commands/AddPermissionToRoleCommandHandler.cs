using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Common.Commands;

public class AddPermissionToRoleCommandHandler : IRequestHandler<AddPermissionToRoleCommand, Unit>
{
    IRoleRepository _roleRepository;

    public AddPermissionToRoleCommandHandler(IUserRepository userRepository, ITeamRepository teamRepository, IMemberRepository memberRepository, IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Unit> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        _roleRepository.AddPermissionToRole(request.Role, request.Permission);
        return Unit.Value;
    }
}
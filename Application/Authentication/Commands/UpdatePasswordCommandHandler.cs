using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.User;
using Domain.User.ValueObjects;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentication.Commands;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UpdatePasswordCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Unit> Handle(UpdatePasswordCommand command, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetUserById(command.id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.VerifyPassword(command.CurrentPassword))
        {
            // Update user Password
            user.ChangeUsername(command.NewPassword);

            _userRepository.Save(user);
        }

        // Return unit
        return Unit.Value;
    }
}

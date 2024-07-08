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

public class UpdateInfoCommandHandler : IRequestHandler<UpdateInfoCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UpdateInfoCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResult> Handle(UpdateInfoCommand command, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetUserById(command.id);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Update user information
        user.ChangeUsername(command.username);
        user.ChangeFirstName(command.firstName);
        user.ChangeLastName(command.lastName);

        _userRepository.Save(user);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateLoginToken(user.Id, user.Username, user.FirstName,
            user.LastName, user.Email, user.Avatar, user.Colour);

        // Return updated user and token
        return new AuthenticationResult(user, token);
    }
}

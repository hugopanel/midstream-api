using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.User;
using Domain.User.ValueObjects;
using MediatR;

namespace Application.Authentication.Commands;

public class ConfirmRegistrationCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<ConfirmRegistrationCommand, AuthenticationResult>
{
    private IUserRepository _userRepository = userRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<AuthenticationResult> Handle(ConfirmRegistrationCommand command, CancellationToken cancellationToken)
    {
        // Create the new user
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = command.username,
            FirstName = command.firstName,
            LastName = command.lastName,
            Email = command.email,
            Password = Password.FromPlainText(command.password),
            Avatar = "1",
            Colour = "#000000"
        };

        // Add new user
        _userRepository.Add(newUser);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateLoginToken(newUser.Id, newUser.Username, newUser.FirstName,
            newUser.LastName, newUser.Email, newUser.Avatar, newUser.Colour);

        // Return new user
        return new AuthenticationResult(newUser, token);
    }
}

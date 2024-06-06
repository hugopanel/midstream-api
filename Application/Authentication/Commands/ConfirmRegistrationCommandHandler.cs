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
            Salt = new byte[] { 0x3E, 0x4D, 0x6C, 0x2A, 0x72, 0x9F, 0x8B, 0x1C, 0x5D, 0x8E, 0x2B, 0x4F, 0x5E, 0x6A, 0x7C, 0x8D },
            Password = new Password(command.password)
        };

        // Add new user
        _userRepository.Add(newUser);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateLoginToken(newUser.Username, newUser.FirstName,
            newUser.LastName, newUser.Email);

        // Return new user
        return new AuthenticationResult(newUser, token);
    }
}
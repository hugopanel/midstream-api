using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using MediatR;

namespace Application.Authentication.Queries;

public class LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private IUserRepository _userRepository = userRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        // Check if user exists with this username and password combination
        // TODO: Hash the password and use the salt

        var user = _userRepository.GetUserByEmailAndPassword(request.Email, request.Password);
        if (user is null)
            throw new Exception("Invalid email or password."); // TODO: Create custom exception

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateLoginToken(user.Id, user.Username, user.FirstName, user.LastName, user.Email);

        // Return new user
        return new AuthenticationResult(user, token);
    }
}
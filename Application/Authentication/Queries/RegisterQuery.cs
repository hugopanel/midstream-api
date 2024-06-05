using MediatR;

namespace Application.Authentication.Queries;

public record RegisterQuery(
    string Username,
    string FirstName,
    string LastName,
    string Email
) : IRequest<AuthenticationResult>;
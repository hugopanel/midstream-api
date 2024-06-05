using MediatR;

namespace Application.Authentication.Queries;

public record LoginQuery(
    string Username,
    string Password) : IRequest<AuthenticationResult>;
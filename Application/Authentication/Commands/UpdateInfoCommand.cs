using MediatR;

namespace Application.Authentication.Commands;

public record UpdateInfoCommand(
    string id,
    string username,
    string firstName,
    string lastName
) : IRequest<AuthenticationResult>;
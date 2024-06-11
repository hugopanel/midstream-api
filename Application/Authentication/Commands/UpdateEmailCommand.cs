using MediatR;

namespace Application.Authentication.Commands;

public record UpdateEmailCommand(
    string id,
    string email
) : IRequest<AuthenticationResult>;
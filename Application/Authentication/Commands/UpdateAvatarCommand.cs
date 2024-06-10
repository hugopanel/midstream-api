using MediatR;

namespace Application.Authentication.Commands;

public record UpdateAvatarCommand(
    string id,
    string Avatar,
    string Colour
) : IRequest<AuthenticationResult>;
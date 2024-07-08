using MediatR;

namespace Application.Authentication.Commands;

public record UpdatePasswordCommand(
    string id,
    string CurrentPassword,
    string NewPassword
) : IRequest<Unit>;
using MediatR;

namespace Application.Teams.Commands;

public record CreateRoleCommand(string Name) : IRequest<RoleResult>;
using MediatR;

namespace Application.Teams.Commands;

public record DeleteMemberRoleCommand(string memberId, string roleId) : IRequest<StringResult>;
using MediatR;

namespace Application.Teams.Commands;

public record CreateMemberRoleCommand(string MemberId, string RoleId) : IRequest<MemberRoleResult>;
using MediatR;

namespace Application.Teams.Commands;

public record CreateMemberCommand(string UserId, string TeamId) : IRequest<MemberResult>;
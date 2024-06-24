using MediatR;

namespace Application.Teams.Commands;

public record DeleteMemberCommand(string memberId) : IRequest<StringResult>;
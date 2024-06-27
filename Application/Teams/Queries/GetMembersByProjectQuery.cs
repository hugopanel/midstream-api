using MediatR;

namespace Application.Teams.Queries;

public record GetMembersByProjectQuery(string projectId) : IRequest<ListMembersToSelectResult>;
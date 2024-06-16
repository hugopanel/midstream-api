using MediatR;

namespace Application.Teams.Queries;

public record GetMembersByTeamQuery(string TeamId) : IRequest<ListMembersResult>;
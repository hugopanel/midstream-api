using MediatR;

namespace Application.Teams.Queries;

public record GetMembersNotInTeamQuery(string TeamId) : IRequest<ListUsersToDisplayResult>;
using MediatR;

namespace Application.Teams.Queries;

public record GetProjectsByUserQuery(string userId) : IRequest<ListProjectsResult>;
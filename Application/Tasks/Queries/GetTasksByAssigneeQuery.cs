using MediatR;

namespace Application.Tasks.Queries;

public record GetTasksByAssigneeQuery(string projectId, string userId) : IRequest<ListTasksResult>;
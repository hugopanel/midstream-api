using MediatR;

namespace Application.Tasks.Queries;

public record GetTasksByPriorityQuery(string projectId, string priority) : IRequest<ListTasksResult>;
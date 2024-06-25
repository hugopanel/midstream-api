using MediatR;

namespace Application.Tasks.Queries;

public record GetTasksByTypeQuery(string projectId, string type) : IRequest<ListTasksResult>;
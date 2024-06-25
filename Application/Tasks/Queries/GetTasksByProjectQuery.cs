using MediatR;

namespace Application.Tasks.Queries;

public record GetTasksByProjectQuery(string projectId) : IRequest<ListTasksResult>;
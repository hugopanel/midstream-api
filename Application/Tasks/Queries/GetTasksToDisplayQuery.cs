using MediatR;

namespace Application.Tasks.Queries;

public record GetTasksToDisplayQuery(string projectId) : IRequest<ListTasksToDisplayResult>;
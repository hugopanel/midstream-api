using MediatR;

namespace Application.Tasks.Queries;

public record GetTasksByEndDateQuery(string projectId, DateTime endDate) : IRequest<ListTasksResult>;
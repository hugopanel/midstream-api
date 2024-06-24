using MediatR;

namespace Application.Tasks.Queries;

public record GetTasksQuery() : IRequest<ListTasksResult>;
using MediatR;

namespace Application.Tasks.Queries;

public record GetTaskToEditQuery(string taskId) : IRequest<TaskToEditResult>;
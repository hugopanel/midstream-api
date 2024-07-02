using MediatR;

namespace Application.Tasks.Commands;

public record DeleteTaskCommand(string taskId) : IRequest<MessageResult>;
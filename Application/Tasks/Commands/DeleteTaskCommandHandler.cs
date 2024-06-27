using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Commands;

public class DeleteTaskCommandHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<DeleteTaskCommand, MessageResult>
{
    private ITaskRepository _TaskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<MessageResult> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        _TaskRepository.Delete(command.taskId);

        // Return new Task
        return new MessageResult("Task deleted successfully");
    }
}
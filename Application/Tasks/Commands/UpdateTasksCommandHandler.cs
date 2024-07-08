using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Commands;

public class UpdateTasksCommandHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<UpdateTasksCommand, MessageResult>
{
    private ITaskRepository _TaskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<MessageResult> Handle(UpdateTasksCommand command, CancellationToken cancellationToken)
    {
        foreach (var task in command.tasks)
        {
            _TaskRepository.Save(task);
        }

        // Return new Task
        return new MessageResult("Tasks updated successfully");
    }
}
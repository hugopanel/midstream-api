using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Commands;

public class CreateTaskCommandHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<CreateTaskCommand, TaskResult>
{
    private ITaskRepository _TaskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<TaskResult> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        // Create the new Task
        var newTask = new Tache
        {
            BeginningDate = command.BeginningDate,
            EndDate = command.EndDate,
            Priority = command.Priority,
            Status = command.Status,
            TypeOfTask = command.TypeOfTask,
            Title = command.Title,
            Description = command.Description,
            Belong = command.Belong,
            Author = command.Author,
            AssignedTo = command.AssignedTo,
            RelatedTo = command.RelatedTo
        };

        // Add new Task
        _TaskRepository.Add(newTask);

        // Return new Task
        return new TaskResult(newTask);
    }
}
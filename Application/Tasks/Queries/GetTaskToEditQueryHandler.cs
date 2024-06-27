using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Tasks;
using MediatR;
using System;
using System.Globalization;

namespace Application.Tasks.Queries;

public class GetTaskToEditToDisplayQueryHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTaskToEditQuery, TaskToEditResult>
{
    private ITaskRepository _taskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<TaskToEditResult> Handle(GetTaskToEditQuery query, CancellationToken cancellationToken)
    {
        var task = _taskRepository.GetTacheById(query.taskId);

        // Return all roles
        return new TaskToEditResult(task.Id.ToString(), task.BeginningDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), task.EndDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), task.Priority, task.Status, task.TypeOfTask, task.Title, task.Description, task.Belong, task.Author, task.AssignedTo, task.RelatedTo);
    }
}
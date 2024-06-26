using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Queries;

public class GetTasksToDisplayToDisplayQueryHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTasksToDisplayQuery, ListTasksToDisplayResult>
{
    private ITaskRepository _taskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTasksToDisplayResult> Handle(GetTasksToDisplayQuery query, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetTachesByProject(query.projectId);

        var types = new List<string>();

        foreach (var task in tasks)
        {
            if(task.TypeOfTask != null && !types.Contains(task.TypeOfTask)){
                types.Add(task.TypeOfTask);
            }
        }

        var priorities = new List<string>();

        foreach (var task in tasks)
        {
            if(task.Priority != null && !priorities.Contains(task.Priority)){
                priorities.Add(task.Priority);
            }
        }

        // Return all roles
        return new ListTasksToDisplayResult(tasks, types, priorities);
    }
}
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Queries;

public class GetTasksToDisplayToDisplayQueryHandler(ITaskRepository taskRepository, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTasksToDisplayQuery, ListTasksToDisplayResult>
{
    private ITaskRepository _taskRepository = taskRepository;
    private IUserRepository _userRepository = userRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTasksToDisplayResult> Handle(GetTasksToDisplayQuery query, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetTachesByProject(query.projectId);

        var tasksToDisplay = new List<TaskToDisplay>();
        var types = new List<string>();
        var priorities = new List<string>();

        foreach (var task in tasks)
        {
            var user = _userRepository.GetUserById(task.AssignedTo);

            var taskToDisplay = new TaskToDisplay(task.Id, task.BeginningDate, task.EndDate, task.Priority, task.Status, task.TypeOfTask, task.Title, task.Description, task.Belong, task.Author, task.AssignedTo, task.RelatedTo, user.Avatar, user.Colour);
            tasksToDisplay.Add(taskToDisplay);

            if (task.TypeOfTask != null && !types.Contains(task.TypeOfTask))
            {
                types.Add(task.TypeOfTask);
            }

            if (task.Priority != null && !priorities.Contains(task.Priority))
            {
                priorities.Add(task.Priority);
            }
        }

        // Return all roles
        return new ListTasksToDisplayResult(tasksToDisplay, types, priorities);
    }
}
using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Queries;

public class GetTasksByPriorityQueryHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTasksByPriorityQuery, ListTasksResult>
{
    private ITaskRepository _taskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTasksResult> Handle(GetTasksByPriorityQuery query, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetTachesByPriority(query.projectId, query.priority);

        // Return all roles
        return new ListTasksResult(tasks);
    }
}
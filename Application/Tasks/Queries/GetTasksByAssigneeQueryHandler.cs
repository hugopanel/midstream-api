using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Queries;

public class GetTasksByAssigneeQueryHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTasksByAssigneeQuery, ListTasksResult>
{
    private ITaskRepository _taskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTasksResult> Handle(GetTasksByAssigneeQuery query, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetTachesByAssignee(query.projectId, query.userId);

        // Return all roles
        return new ListTasksResult(tasks);
    }
}
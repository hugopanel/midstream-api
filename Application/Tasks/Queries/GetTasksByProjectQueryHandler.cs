using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Queries;

public class GetTasksByProjectQueryHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTasksByProjectQuery, ListTasksResult>
{
    private ITaskRepository _taskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTasksResult> Handle(GetTasksByProjectQuery query, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetTachesByProject(query.projectId);

        // Return all roles
        return new ListTasksResult(tasks);
    }
}
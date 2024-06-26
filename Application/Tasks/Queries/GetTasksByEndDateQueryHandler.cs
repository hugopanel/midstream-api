using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.Queries;

public class GetTasksByEndDateQueryHandler(ITaskRepository taskRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTasksByEndDateQuery, ListTasksResult>
{
    private ITaskRepository _taskRepository = taskRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTasksResult> Handle(GetTasksByEndDateQuery query, CancellationToken cancellationToken)
    {
        var tasks = _taskRepository.GetTachesByEndDate(query.projectId, query.endDate);

        // Return all roles
        return new ListTasksResult(tasks);
    }
}
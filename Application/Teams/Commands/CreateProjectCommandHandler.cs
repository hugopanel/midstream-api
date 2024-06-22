using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class CreateProjectCommandHandler(IProjectRepository projectRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<CreateProjectCommand, ProjectResult>
{
    private IProjectRepository _ProjectRepository = projectRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ProjectResult> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        // Create the new Project
        var newProject = new Project
        {
            Id = 0,
            Name = command.Name,
            Description = command.Description,
            Beginning_date = DateTime.Now.ToUniversalTime()
        };

        // Add new Project
        _ProjectRepository.Add(newProject);

        // Return new Project
        return new ProjectResult(newProject);
    }
}
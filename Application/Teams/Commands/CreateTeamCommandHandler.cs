using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Commands;

public class CreateTeamCommandHandler(ITeamRepository teamRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<CreateTeamCommand, TeamResult>
{
    private ITeamRepository _teamRepository = teamRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<TeamResult> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
    {
        // Create the new team
        var newTeam = new Team
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            ProjectId = Guid.Parse(command.ProjectId),
        };

        // Add new team
        _teamRepository.Add(newTeam);

        // Return new team
        return new TeamResult(newTeam);
    }
}
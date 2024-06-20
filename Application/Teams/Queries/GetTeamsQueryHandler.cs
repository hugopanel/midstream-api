using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MediatR;

namespace Application.Teams.Queries;

public class GetTeamsQueryHandler(ITeamRepository teamRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTeamsQuery, ListTeamsResult>
{
    private ITeamRepository _teamRepository = teamRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTeamsResult> Handle(GetTeamsQuery query, CancellationToken cancellationToken)
    {
        var teams = _teamRepository.GetTeams();

        // Return all Teams
        return new ListTeamsResult(teams);
    }
}
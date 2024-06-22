using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Teams;
using MediatR;

namespace Application.Teams.Queries;

public class GetTeamNameByIdQueryHandler(ITeamRepository teamRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTeamNameByIdQuery, StringResult>
{
    private ITeamRepository _TeamRepository = teamRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<StringResult> Handle(GetTeamNameByIdQuery query, CancellationToken cancellationToken)
    {
        string teamName = _TeamRepository.GetTeamNameById(query.teamId);

        // Return new Team
        return new StringResult(teamName);
    }
}
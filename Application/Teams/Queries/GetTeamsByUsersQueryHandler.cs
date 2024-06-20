using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Application.Teams;
using MediatR;

namespace Application.Teams.Queries;

public class GetTeamsByUserQueryHandler(IMemberRepository memberRepository, ITeamRepository teamRepository, IJwtTokenGenerator jwtTokenGenerator)
    : IRequestHandler<GetTeamsByUserQuery, ListTeamsResult>
{
    private IMemberRepository _MemberRepository = memberRepository;
    private ITeamRepository _TeamRepository = teamRepository;
    private IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

    public async Task<ListTeamsResult> Handle(GetTeamsByUserQuery query, CancellationToken cancellationToken)
    {
        var teamsId = _MemberRepository.GetTeamsIdByUserId(query.userId);

        var teams = new List<Team>();

        for(var i = 0; i < teamsId.Count; i++)
        {
            var team  = _TeamRepository.GetTeamById(teamsId[i].ToString());
            
            teams.Add(team);
        }

        // Return new Team
        return new ListTeamsResult(teams);
    }
}
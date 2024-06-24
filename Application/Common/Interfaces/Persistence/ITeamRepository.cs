using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ITeamRepository
{

    List<Team> GetTeams();

    Team? GetTeamById(string id);
    string GetTeamNameById(string id);

    void Add(Team team);
    void Save(Team team);
    void Delete(Team team);
}
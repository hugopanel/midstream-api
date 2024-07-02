using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ITeamRepository
{

    List<Team> GetTeams();

    Team? GetTeamById(string id);
    Team? GetTeamByProjectId(string projectId);
    string GetTeamNameById(string id);
    string GetProjectIdByTeamId(string id);

    void Add(Team team);
    void Save(Team team);
    void Delete(Team team);
}
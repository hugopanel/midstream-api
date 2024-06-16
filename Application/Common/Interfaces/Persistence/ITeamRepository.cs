using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ITeamRepository
{
    Team? GetTeamById(string id);

    void Add(Team team);
    void Save(Team team);
}
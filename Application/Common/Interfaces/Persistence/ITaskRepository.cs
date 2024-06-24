using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ITaskRepository
{
    List<Tache> GetTaches();

    Tache? GetTacheById(string id);

    void Add(Tache task);

    void Save(Tache task);
}
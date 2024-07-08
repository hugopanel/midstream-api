using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface ITaskRepository
{
    List<Tache> GetTaches();
    List<Tache> GetTachesByAssignee(string projectId, string userId);
    List<Tache> GetTachesByProject(string projectId);
    List<Tache> GetTachesByType(string projectId, string type);
    List<Tache> GetTachesByEndDate(string projectId, DateTime endDate);
    List<Tache> GetTachesByPriority(string projectId, string priority);

    Tache? GetTacheById(string id);

    void Add(Tache task);

    void Save(Tache task);

    void Delete(string id);
}
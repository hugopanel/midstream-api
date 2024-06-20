using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IProjectRepository
{
    Project? GetProjectById(string id);

    List<Project> GetProjects();

    void Add(Project project);
    void Save(Project project);
}
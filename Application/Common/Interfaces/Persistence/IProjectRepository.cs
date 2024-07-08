using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IProjectRepository
{
    Project? GetProjectById(string id);
    Project? GetProjectByName(string name);

    List<Project> GetProjects();
    List<Project> GetProjectsWithoutTeam();

    void Add(Project project);
    void Save(Project project);
}

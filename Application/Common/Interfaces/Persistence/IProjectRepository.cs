using Domain.Entities;
namespace Application.Common.Interfaces.Persistence;

public interface IProjectRepository
{
    List<Project>? GetAllProjects();
}

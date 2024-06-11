using System.Text.Json;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Data;
public class ProjectRepository : IProjectRepository
{
    // private readonly MongoDbContext _dbContext;
    // public ProjectRepository(MongoDbContext dbContext)
    // {
    //     _dbContext = dbContext;
    // }
    public List<Project>? GetAllProjects()
    {
        return JsonSerializer.Deserialize<List<Project>>(File.ReadAllText("../Infrastructure/Persistence/projects.json"));

    }

}

using System.Text.Json;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Data;
public class ModuleRepository : IModuleRepository
{
    // private readonly MongoDbContext _dbContext;

    // public ModuleRepository(MongoDbContext dbContext)
    // {
    //     _dbContext = dbContext;
    // }
    public List<Module>? GetAllModules()
    {
        return JsonSerializer.Deserialize<List<Module>>(File.ReadAllText("../Infrastructure/Persistence/modules.json"));

    }
}

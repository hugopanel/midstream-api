using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Data;
public class FileRepository : IFileRepository
{
    // private readonly MongoDbContext _dbContext;

    // public FileRepository(MongoDbContext dbContext)
    // {
    //     _dbContext = dbContext;
    // }

    public List<FileApp>? GetAllFiles()
    {
        return JsonSerializer.Deserialize<List<FileApp>>(File.ReadAllText("../Infrastructure/Persistence/files.json"));
    }
}

using System.Text.Json;
using System.Text.Json.Nodes;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using MongoDB.Driver;


namespace Infrastructure.MongoDb;
public class FileRepository : IFileRepository
{
    private readonly MongoDbContext _dbContext;

    public FileRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public List<FileApp>? GetFiles(string Belong)
    {
        return _dbContext.GetCollection<FileApp>("myCollection").Find(t => t.Belong == Belong).ToList();
    }

    public FileApp GetFile(string Id)
    {
        return _dbContext.GetCollection<FileApp>("myCollection").Find(t => t.Id == Id).SingleOrDefault();
    }

    public void AddFile(FileApp fileDb)
    {
        try
        {
 
             _dbContext.GetCollection<FileApp>("myCollection").InsertOne(fileDb);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }


    }
    public void DeleteFile(FileApp fileDb)
    {
        _dbContext.GetCollection<FileApp>("myCollection").DeleteOne(t => t.Id == fileDb.Id);
    }
}

// using Application.Common.Interfaces.Persistence;
// using Domain.Entities;

// namespace Infrastructure.Data;
// public class FileRepository : IFileRepository
// {
//     private readonly MongoDbContext _dbContext;

//     public FileRepository(MongoDbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }

//     public FileApp? GetFileById(string id)
//     {
//         return _dbContext.Files.SingleOrDefault(f => f.Id.ToString() == id);
//     }

//     public FileApp? GetFileByName(string name)
//     {
//         return _dbContext.Files.SingleOrDefault(f => f.Name == name);
//     }

//     public List<FileApp>? GetAllFiles()
//     {
//         return _dbContext.Files.ToList();
//     }

//     public void Add(FileApp file)
//     {
//         _dbContext.Files.Add(file);
//         _dbContext.SaveChanges();
//     }

//     public void Save(FileApp file)
//     {
//         _dbContext.Files.Update(file);
//         _dbContext.SaveChanges();
//     }

//     public void Delete(FileApp file)
//     {
//         _dbContext.Files.Remove(file);
//         _dbContext.SaveChanges();
//     }

// }

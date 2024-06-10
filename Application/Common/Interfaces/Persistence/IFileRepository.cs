using Domain.Entities;
namespace Application.Common.Interfaces.Persistence;

public interface IFileRepository
{
    FileApp? GetFileById(string id);
    FileApp? GetFileByName(string name);
    List<FileApp>? GetAllFiles();
    void Add(FileApp file);
    void Save(FileApp file);
    void Delete(FileApp file);

}

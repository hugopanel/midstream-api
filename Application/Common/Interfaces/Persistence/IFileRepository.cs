using Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Application.Common.Interfaces.Persistence;

public interface IFileRepository
{
    List<FileApp>? GetFiles(string idProject);
    FileApp GetFile(string idFile);
    void AddFile(FileApp fileDb);
    void DeleteFile(FileApp fileDb);

}

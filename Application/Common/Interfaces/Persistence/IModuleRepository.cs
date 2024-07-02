using Domain.Entities;
namespace Application.Common.Interfaces.Persistence;

public interface IModuleRepository
{
    Module? GetModuleById(int id);
    Module? GetModuleByName(string name);
    List<Module> GetModules();
    List<Module> GetModulesByNames(string[] names);
    void Add(Module module);
    void Save(Module module);
}

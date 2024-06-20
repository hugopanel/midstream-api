using System.Reflection;

namespace Domain.Interfaces;

public interface IModuleHandler
{
    List<IModule> Modules { get; set; }
    
    void AddModule(IModule module);
    void LoadModulesFromAssemblies(IEnumerable<Assembly> assemblies);
}
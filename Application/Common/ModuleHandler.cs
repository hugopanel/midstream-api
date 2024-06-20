using System.Reflection;
using Domain.Interfaces;

namespace Application.Common;

public class ModuleHandler : IModuleHandler
{
    public List<IModule> Modules { get; set; } = new();
    
    public void AddModule(IModule module)
    {
        Modules.Add(module);
    }

    public void LoadModulesFromAssemblies(IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var moduleTypes = assembly.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsInterface);
            foreach (var moduleType in moduleTypes)
            {
                if (Activator.CreateInstance(moduleType) is IModule module)
                    AddModule(module);
            }
        }
    }
}
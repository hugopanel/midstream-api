using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleA;

public class MyModuleClass : IModule
{
    public string Name { get; set; } = "ModuleA";
    public string Description { get; set; } = "This is a module for testing purposes.";
    public string Author { get; set; } = "John Doe";
    public List<Permission> Permissions { get; set; } = new();
    public string FrontPath { get; set; } = "moduleafront";
    public string RoutePrefix { get; set; } = "modulea";
    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine("Configuring services for ModuleA");
    }
}
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleB;

public class MyModuleClass : IModule
{
    public string Name { get; set; } = "ModuleB";
    public string Description { get; set; } = "This is another module for testing purposes.";
    public string Author { get; set; } = "John Doe";
    public List<Permission> Permissions { get; set; } = new();
    public string FrontPath { get; set; } = "modulebfront";
    public string RoutePrefix { get; set; } = "moduleb";
    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine("Configuring services for ModuleB");
    }
}
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Interfaces;

public interface IModule
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Author { get; set; } // Replace with a "Module Author" object
    public List<Permission> Permissions { get; set; } // List of new permissions defined by the module
    public string FrontPath { get; set; } // The path to the front-end files
    public string RoutePrefix { get; set; } // The prefix for the API routes (e.g. api:port/mymodule/action)

    void ConfigureServices(IServiceCollection services);
    void ConfigureApp(WebApplication app);
}
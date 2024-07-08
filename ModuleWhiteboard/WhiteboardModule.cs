using Application.Whiteboard;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ModuleA;

public class WhiteboardModule : IModule
{
    public string Name { get; set; } = "Whiteboard";
    public string Description { get; set; } = "This module adds a whiteboard to draw collaboratively.";
    public string Author { get; set; } = "Midstream";
    public List<Permission> Permissions { get; set; } = new();
    public string FrontPath { get; set; } = "midstream.whiteboard";
    public string RoutePrefix { get; set; } = "midstream/whiteboard";
    public void ConfigureServices(IServiceCollection services)
    {
        Console.WriteLine("Configuring services for module Whiteboard");
        services.AddSignalR();
    }

    public void ConfigureApp(WebApplication app)
    {
        // app.AddSignalR();
        app.MapHub<WhiteboardHub>("hubs/whiteboard");
    }
}
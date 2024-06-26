using System.Reflection;
using System.Runtime.Loader;
using Api;
using Application;
using Application.Common;
using Application.Whiteboard;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

ModuleHandler moduleHandler;

var builder = WebApplication.CreateBuilder(args);
{
    // Add CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });
    
    // Load modules
    Console.WriteLine("Loading modules...");
    moduleHandler = new ModuleHandler();
    var modulePath = Path.Combine(AppContext.BaseDirectory, "Modules");
    if (!Directory.Exists(modulePath))
        Directory.CreateDirectory(modulePath);

    // Load the assemblies
    List<Assembly> assemblies = new();
    foreach (var moduleFile in Directory.GetFiles(modulePath, "*.dll"))
    {
        AssemblyLoadContext assemblyLoadContext = new(moduleFile);
        var assembly = assemblyLoadContext.LoadFromAssemblyPath(moduleFile);
        assemblies.Add(assembly);
    }
    
    moduleHandler.LoadModulesFromAssemblies(assemblies);
    
    // Add controllers from modules
    var controllerBuilder = builder.Services.AddControllers();
    foreach (var module in moduleHandler.Modules)
    {
        var assembly = module.GetType().Assembly;
        controllerBuilder.AddApplicationPart(assembly);
    }

    // Add module route conventions (prefixes to avoid conflits)
    builder.Services.Configure<MvcOptions>(options =>
    {
        foreach (var module in moduleHandler.Modules)
        {
            var assembly = module.GetType().Assembly;
            options.Conventions.Add(new ModuleRouteConvention(module.RoutePrefix, assembly.GetName().Name));
        }
    });
    Console.WriteLine("Modules loaded.");
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Api", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "JWT Bearer Token",
            // Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                }, 
                new List<string>()
            }
        });
    });


    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddAuthentication();

    builder.Services.AddSignalR();

    // Let modules configure services
    foreach (var module in moduleHandler.Modules)
    {
        module.ConfigureServices(builder.Services);
    }
}

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapHub<WhiteboardHub>("/hubs/whiteboard");

app.Run();

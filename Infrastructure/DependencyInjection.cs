using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Email;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Email;
using Infrastructure.Repositories;
using Infrastructure.MongoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Infrastructure.Configuration;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddHttpClient<IPredictionService, PredictionService>();

            services.AddAuth(configuration);

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("postgres")));

            services.AddSingleton<MongoDbContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.Configure<FlaskApiSettings>(configuration.GetSection("FlaskApi")); 
            
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }
    }
}
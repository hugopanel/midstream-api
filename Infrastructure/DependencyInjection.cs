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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddAuth(configuration);

            services.AddDbContext<UserDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("postgres")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailService, EmailService>();


            // Configuration MongoDB
            // var mongoDbSettings = new MongoDbSettings();
            // configuration.GetSection("MongoDB").Bind(mongoDbSettings);

            // services.AddSingleton<IMongoClient, MongoClient>(sp =>
            // {
            //     return new MongoClient(mongoDbSettings.ConnectionString);
            // });

            // services.AddSingleton(sp =>
            // {
            //     var client = sp.GetRequiredService<IMongoClient>();
            //     return client.GetDatabase(mongoDbSettings.DatabaseName);
            // });
            // services.AddScoped<IFileRepository, FileRepository>();
            
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
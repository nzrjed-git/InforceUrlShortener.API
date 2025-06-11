using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Domain.ServicesInterfaces;
using InforceUrlShortener.Infrastructure.Authorization.Services;
using InforceUrlShortener.Infrastructure.Persistence;
using InforceUrlShortener.Infrastructure.Repositories;
using InforceUrlShortener.Infrastructure.Seeders;
using InforceUrlShortener.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace InforceUrlShortener.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnectionString = configuration.GetConnectionString("InforceUrlShortenerDb");
            if (dbConnectionString.IsNullOrEmpty())
            {
                throw new InvalidOperationException("No database connection string configured");
            }
            

            services.AddDbContext<InforceUrlShortenerDbContext>(opt =>
                opt.UseSqlServer(dbConnectionString)
                .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<InforceUrlShortenerDbContext>();

            services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();
            services.AddScoped<IUrlShortenerService, UrlShortenerService>();
            services.AddScoped<IUrlShortenerSeeder, UrlShortenerSeeder>();
            services.AddScoped<IShortenedUrlsAuthorizationService, ShortenedUrlsAuthorizationService>();
        }
    }
}

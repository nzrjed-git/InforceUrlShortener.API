using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Domain.ServicesInterfaces;
using InforceUrlShortener.Infrastructure.Persistence;
using InforceUrlShortener.Infrastructure.Repositories;
using InforceUrlShortener.Infrastructure.Services;
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
                opt.UseSqlServer(dbConnectionString));

            services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();
            services.AddScoped<IUrlShortenerService, UrlShortenerService>();
        }
    }
}

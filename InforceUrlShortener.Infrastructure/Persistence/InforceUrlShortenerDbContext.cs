using InforceUrlShortener.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InforceUrlShortener.Infrastructure.Persistence
{
    public class InforceUrlShortenerDbContext(DbContextOptions<InforceUrlShortenerDbContext> options) 
        : DbContext(options)
    {
        internal DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    }
}

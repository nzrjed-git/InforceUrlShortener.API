using InforceUrlShortener.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InforceUrlShortener.Infrastructure.Persistence
{
    public class InforceUrlShortenerDbContext(DbContextOptions<InforceUrlShortenerDbContext> options) 
        : IdentityDbContext<User>(options)
    {
        internal DbSet<ShortenedUrl> ShortenedUrls { get; set; }
        internal DbSet<AlgorithmDescription> AlgorithmDescription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ShortenedUrls)
                .WithOne(s => s.Owner)
                .HasForeignKey(s => s.OwnerId);
        }
    }
}

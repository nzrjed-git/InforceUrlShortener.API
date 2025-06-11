using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InforceUrlShortener.Infrastructure.Repositories
{
    public class ShortenedUrlRepository(InforceUrlShortenerDbContext dbContext) : IShortenedUrlRepository
    {
        
        public async Task<Guid> CreateAsync(ShortenedUrl shortenedUrl)
        {
            await dbContext.AddAsync(shortenedUrl);
            await dbContext.SaveChangesAsync();
            return shortenedUrl.Id;
        }

        public async Task DeleteAsync(ShortenedUrl shortenedUrl)
        {
            dbContext.Remove(shortenedUrl);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ShortenedUrl?> GetOriginalUrlByShortCode(string shortCode)
        {
            return await dbContext.ShortenedUrls
                .AsNoTracking()
                .FirstOrDefaultAsync(s=>s.ShortCode == shortCode);
        }

        public async Task<(IEnumerable<ShortenedUrl>, int)> GetPaginatedShortenedUrlsAsync(int pageSize, int pageNumber)
        {
            var query = dbContext.ShortenedUrls.AsNoTracking().AsQueryable();
            var totalCount = await query.CountAsync();
            var shortenedUrls = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (shortenedUrls, totalCount);
        }

        public async Task<ShortenedUrl?> GetShortenedUrlById(Guid id)
        {
            return await dbContext.ShortenedUrls
                .AsNoTracking()
                .Include(s=>s.Owner)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> HasOriginalUrlDuplicateAsync(string originalUrl)
        {
            return await dbContext.ShortenedUrls.AnyAsync(s => s.OriginalUrl == originalUrl);
        }

        public async Task<bool> HasShortCodeDuplicateAsync(string shortCode)
        {
            return await dbContext.ShortenedUrls.AnyAsync(s=>s.ShortCode == shortCode);
        }
    }
}

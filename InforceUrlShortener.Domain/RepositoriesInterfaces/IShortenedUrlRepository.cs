using InforceUrlShortener.Domain.Entities;

namespace InforceUrlShortener.Domain.RepositoriesInterfaces
{
    public interface IShortenedUrlRepository
    {
        Task CreateAsync(ShortenedUrl shortenedUrl);
        Task<bool> HasOriginalUrlDuplicateAsync(string originalUrl);
        Task<bool> HasShortCodeDuplicateAsync(string shortCode);
        Task<ShortenedUrl?> GetShortenedUrlById(Guid id);
        Task<(IEnumerable<ShortenedUrl>, int)> GetPaginatedShortenedUrlsAsync(int pageSize, int pageNumber);
        Task DeleteAsync(ShortenedUrl shortenedUrl);
        Task<ShortenedUrl?> GetOriginalUrlByShortCode(string shortCode);
    }
}

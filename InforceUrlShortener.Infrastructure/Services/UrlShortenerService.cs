using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Domain.ServicesInterfaces;

namespace InforceUrlShortener.Infrastructure.Services
{
    public class UrlShortenerService(IShortenedUrlRepository shortenedUrlRepository) : IUrlShortenerService
    {
        public async Task<string> GenerateShortCodeAsync()
        {
            string shortCode;
            do
            {
                shortCode = Guid.NewGuid().ToString("N").Substring(0, 8);
            }
            while (await shortenedUrlRepository.HasShortCodeDuplicateAsync(shortCode));

            return shortCode;
        }
    }
}

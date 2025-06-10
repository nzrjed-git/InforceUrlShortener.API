using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Domain.ServicesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl
{
    public class CreateShortenedUrlCommandHandler(
        IShortenedUrlRepository shortenedUrlRepository,
        IUrlShortenerService urlShortenerService) 
        : IRequestHandler<CreateShortenedUrlCommand, Guid>
    {
        public async Task<Guid> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            var shortCode = await urlShortenerService.GenerateShortCodeAsync();
            var hasOriginalUrlDuplicate = await shortenedUrlRepository.HasOriginalUrlDuplicateAsync(request.OriginalUrl);
            if (hasOriginalUrlDuplicate)
                throw new OriginalUrlDuplicateException(request.OriginalUrl);

            var shortenedUrl = new ShortenedUrl
            {
                OriginalUrl = request.OriginalUrl,
                ShortCode = shortCode,
                CreatedAt = DateTime.UtcNow,
            };

            var id = await shortenedUrlRepository.CreateAsync(shortenedUrl);
            return id;
        }
    }
}

using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Queries.GetOriginalUrlByShortCode
{
    public class GetOriginalUrlByShortCodeQueryHandler(
        IShortenedUrlRepository shortenedUrlRepository)
        : IRequestHandler<GetOriginalUrlByShortCodeQuery, string?>
    {
        public async Task<string?> Handle(GetOriginalUrlByShortCodeQuery request, CancellationToken cancellationToken)
        {
            var shortenedUrl = await shortenedUrlRepository.GetOriginalUrlByShortCode(request.ShortCode)
                ?? throw new NotFoundByShortCodeException(nameof(ShortenedUrl), request.ShortCode);

            return shortenedUrl.OriginalUrl;
        }
    }
}

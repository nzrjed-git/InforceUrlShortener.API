using InforceUrlShortener.Application.ShortenedUrls.DTOs;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl
{
    public class CreateShortenedUrlCommand : IRequest<ShortenedUrlListItemDto>
    {
        public string OriginalUrl { get; set; } = default!;
    }
}

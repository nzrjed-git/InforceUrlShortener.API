using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl
{
    public class CreateShortenedUrlCommand : IRequest<Guid>
    {
        public string OriginalUrl { get; set; } = default!;
    }
}

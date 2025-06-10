using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.DeleteShortenedUrl
{
    public class DeleteShortenedUrlCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

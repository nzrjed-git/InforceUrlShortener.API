using InforceUrlShortener.Application.ShortenedUrls.DTOs;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Queries.GetShortenedUrlById
{
    public class GetShortenedUrlByIdQuery: IRequest<ShortenedUrlFullDto>
    {
        public Guid Id { get; set; }
    }
}

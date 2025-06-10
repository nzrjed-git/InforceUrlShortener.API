using InforceUrlShortener.Application.Common;
using InforceUrlShortener.Application.ShortenedUrls.DTOs;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Queries.GetPaginatedShortenedUrls
{
    public class GetPaginatedShortenedUrlsQuery : IRequest<PagedResult<ShortenedUrlListItemDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

using AutoMapper;
using InforceUrlShortener.Application.Common;
using InforceUrlShortener.Application.ShortenedUrls.DTOs;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Queries.GetPaginatedShortenedUrls
{
    public class GetPaginatedShortenedUrlsQueryHandler(
        IShortenedUrlRepository shortenedUrlRepository,
        IMapper mapper)
        : IRequestHandler<GetPaginatedShortenedUrlsQuery, PagedResult<ShortenedUrlListItemDto>>
    {
        public async Task<PagedResult<ShortenedUrlListItemDto>> Handle(GetPaginatedShortenedUrlsQuery request, CancellationToken cancellationToken)
        {
            var (shortenedUrls, totalCount) = await shortenedUrlRepository
                .GetPaginatedShortenedUrlsAsync(request.PageSize, request.PageNumber);
            var shortenedUrlsListItemsDtos = mapper.Map<IEnumerable<ShortenedUrlListItemDto>>(shortenedUrls);
            var result = new PagedResult<ShortenedUrlListItemDto>(
                shortenedUrlsListItemsDtos,
                totalCount,
                request.PageSize,
                request.PageNumber);

            return result;
        }
    }
}

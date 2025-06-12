using AutoMapper;
using InforceUrlShortener.Application.Helpers;
using InforceUrlShortener.Application.ShortenedUrls.DTOs;
using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Queries.GetShortenedUrlById
{
    public class GetShortenedUrlByIdQueryHandler(
        IShortenedUrlRepository shortenedUrlRepository,
        IMapper mapper)
        : IRequestHandler<GetShortenedUrlByIdQuery, ShortenedUrlFullDto>
    {
        public async Task<ShortenedUrlFullDto> Handle(GetShortenedUrlByIdQuery request, CancellationToken cancellationToken)
        {
            var shortenedUrl = await shortenedUrlRepository.GetShortenedUrlById(request.Id)
                ?? throw new NotFoundException(nameof(ShortenedUrl), request.Id.ToString());

            var shortenedUrlFullDto = mapper.Map<ShortenedUrlFullDto>(shortenedUrl);
            
            shortenedUrlFullDto.OwnerEmail = shortenedUrl.Owner.Email!;
            shortenedUrlFullDto.CreatedAt = TimeZoneHelper.ConvertUtcToKyiv(shortenedUrl.CreatedAt);

            return shortenedUrlFullDto;
        }
    }
}

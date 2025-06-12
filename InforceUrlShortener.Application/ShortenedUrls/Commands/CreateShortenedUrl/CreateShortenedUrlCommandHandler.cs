using AutoMapper;
using InforceUrlShortener.Application.ShortenedUrls.DTOs;
using InforceUrlShortener.Application.User;
using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Domain.ServicesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl
{
    public class CreateShortenedUrlCommandHandler(
        IShortenedUrlRepository shortenedUrlRepository,
        IUrlShortenerService urlShortenerService,
        IUserContext userContext,
        IMapper mapper) 
        : IRequestHandler<CreateShortenedUrlCommand, ShortenedUrlListItemDto>
    {
        public async Task<ShortenedUrlListItemDto> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser()!;

            var shortCode = await urlShortenerService.GenerateShortCodeAsync();
            var hasOriginalUrlDuplicate = await shortenedUrlRepository.HasOriginalUrlDuplicateAsync(request.OriginalUrl);
            if (hasOriginalUrlDuplicate)
                throw new OriginalUrlDuplicateException(request.OriginalUrl);

            var shortenedUrl = new ShortenedUrl
            {
                OriginalUrl = request.OriginalUrl,
                ShortCode = shortCode,
                CreatedAt = DateTime.UtcNow,
                OwnerId = currentUser.Id
            };

            await shortenedUrlRepository.CreateAsync(shortenedUrl);

            var shortenedUrlListItemDto = mapper.Map<ShortenedUrlListItemDto>(shortenedUrl);
            return shortenedUrlListItemDto;
        }
    }
}

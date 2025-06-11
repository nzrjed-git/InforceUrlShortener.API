using InforceUrlShortener.Application.User;
using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Domain.ServicesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.DeleteShortenedUrl
{
    public class DeleteShortenedUrlCommandHandler(
        IShortenedUrlRepository shortenedUrlRepository,
        IShortenedUrlsAuthorizationService shortenedUrlsAuthorizationService)
        : IRequestHandler<DeleteShortenedUrlCommand>
    {
        public async Task Handle(DeleteShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            
            var shortenedUrl = await shortenedUrlRepository.GetShortenedUrlById(request.Id)
                ?? throw new NotFoundException(nameof(ShortenedUrl), request.Id.ToString());

            if(!shortenedUrlsAuthorizationService
                .Authorize(shortenedUrl, Domain.Constants.ResourceOperation.Delete))
            {
                throw new ForbidException();
            }
                
            await shortenedUrlRepository.DeleteAsync(shortenedUrl);
        }
    }
}

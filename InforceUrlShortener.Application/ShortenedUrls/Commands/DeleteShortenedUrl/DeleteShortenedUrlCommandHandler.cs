using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Commands.DeleteShortenedUrl
{
    public class DeleteShortenedUrlCommandHandler(
        IShortenedUrlRepository shortenedUrlRepository)
        : IRequestHandler<DeleteShortenedUrlCommand>
    {
        public async Task Handle(DeleteShortenedUrlCommand request, CancellationToken cancellationToken)
        {
            var shortenedUrl = await shortenedUrlRepository.GetShortenedUrlById(request.Id)
                ?? throw new NotFoundException(nameof(ShortenedUrl), request.Id.ToString());

            await shortenedUrlRepository.DeleteAsync(shortenedUrl);
        }
    }
}

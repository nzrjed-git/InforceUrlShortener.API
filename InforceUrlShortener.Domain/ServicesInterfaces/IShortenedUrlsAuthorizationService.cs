using InforceUrlShortener.Domain.Constants;
using InforceUrlShortener.Domain.Entities;

namespace InforceUrlShortener.Domain.ServicesInterfaces
{
    public interface IShortenedUrlsAuthorizationService
    {
        bool Authorize(ShortenedUrl shortenedUrl, ResourceOperation operation);
    }
}

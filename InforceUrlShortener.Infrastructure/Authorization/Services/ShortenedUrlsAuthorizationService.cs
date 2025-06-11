using InforceUrlShortener.Application.User;
using InforceUrlShortener.Domain.Constants;
using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.ServicesInterfaces;

namespace InforceUrlShortener.Infrastructure.Authorization.Services
{
    public class ShortenedUrlsAuthorizationService(
        IUserContext userContext) : IShortenedUrlsAuthorizationService
    {
        public bool Authorize(ShortenedUrl shortenedUrl, ResourceOperation operation)
        {
            var user = userContext.GetCurrentUser()!;

            if (operation == ResourceOperation.Delete && 
                (user.IsInRole(UserRoles.Admin) || user.Id == shortenedUrl.OwnerId))
            {
                return true;
            }
            return false;
        }
    }
}

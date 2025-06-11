using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace InforceUrlShortener.Application.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }

    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = (httpContextAccessor?.HttpContext?.User)
                ?? throw new InvalidOperationException("User context is null");

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var role = user.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;

            return new CurrentUser(userId, email, role);
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace InforceUrlShortener.Domain.Entities
{
    public class User : IdentityUser
    {
        public List<ShortenedUrl> ShortenedUrls { get; set; } = new();
    }
}

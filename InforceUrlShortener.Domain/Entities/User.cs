namespace InforceUrlShortener.Domain.Entities
{
    public class User
    {
        public List<ShortenedUrl> ShortenedUrls { get; set; } = new();
    }
}

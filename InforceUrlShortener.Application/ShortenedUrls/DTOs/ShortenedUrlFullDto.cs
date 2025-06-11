namespace InforceUrlShortener.Application.ShortenedUrls.DTOs
{
    public class ShortenedUrlFullDto
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; } = default!;
        public string ShortCode { get; set; } = default!;
        public DateTime CreatedAt { get; set; }

        public string OwnerEmail { get; set; } = default!;
    }
}

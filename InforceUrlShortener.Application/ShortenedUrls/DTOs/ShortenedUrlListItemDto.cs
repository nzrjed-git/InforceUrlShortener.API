namespace InforceUrlShortener.Application.ShortenedUrls.DTOs
{
    public class ShortenedUrlListItemDto
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; } = default!;
        public string ShortCode { get; set; } = default!;
        public string OwnerId { get; set; } = default!;
    }
}

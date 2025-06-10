namespace InforceUrlShortener.Domain.Entities
{
    public class ShortenedUrl
    {
        public Guid Id { get; set; }

        public string OriginalUrl { get; set; } = default!;

        public string ShortCode { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        //public Guid OwnerId { get; set; }
        //public User Owner { get; set; }
    }
}

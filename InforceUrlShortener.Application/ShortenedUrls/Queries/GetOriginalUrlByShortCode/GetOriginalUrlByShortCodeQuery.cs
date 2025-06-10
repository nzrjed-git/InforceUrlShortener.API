using MediatR;

namespace InforceUrlShortener.Application.ShortenedUrls.Queries.GetOriginalUrlByShortCode
{
    public class GetOriginalUrlByShortCodeQuery : IRequest<string?>
    {
        public string ShortCode { get; set; } = string.Empty;
    }
}

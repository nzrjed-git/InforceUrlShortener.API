namespace InforceUrlShortener.Domain.Exceptions
{
    public class OriginalUrlDuplicateException(string originalUrl)
        : Exception($"Url: {originalUrl} already has shortened url. You can find it by original url");
}

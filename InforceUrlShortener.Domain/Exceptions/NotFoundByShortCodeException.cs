namespace InforceUrlShortener.Domain.Exceptions
{
    public class NotFoundByShortCodeException(string resourceType, string shortCode)
        : Exception($"{resourceType} with short code: {shortCode} does not exist")
    {
    }
}

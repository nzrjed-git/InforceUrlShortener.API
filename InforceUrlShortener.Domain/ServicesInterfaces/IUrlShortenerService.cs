namespace InforceUrlShortener.Domain.ServicesInterfaces
{
    public interface IUrlShortenerService
    {
        Task<string> GenerateShortCodeAsync();
    }
}

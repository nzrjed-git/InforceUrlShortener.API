using InforceUrlShortener.Domain.Entities;

namespace InforceUrlShortener.Domain.RepositoriesInterfaces
{
    public interface IAlgorithmDescriptionRepository
    {
        Task SaveChangesAsync();
        Task<AlgorithmDescription?> GetAsync();
    }
}

using InforceUrlShortener.Domain.Entities;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using InforceUrlShortener.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InforceUrlShortener.Infrastructure.Repositories
{
    public class AlgorithmDescriptionRepository(
        InforceUrlShortenerDbContext dbContext) : IAlgorithmDescriptionRepository
    {
        public async Task<AlgorithmDescription?> GetAsync()
        {
            return await dbContext.AlgorithmDescription.FirstOrDefaultAsync(a=>a.Id == AlgorithmDescription.SingletonId);
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}

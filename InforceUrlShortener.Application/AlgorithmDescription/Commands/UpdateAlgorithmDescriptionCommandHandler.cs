using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.AlgorithmDescription.Commands
{
    public class UpdateAlgorithmDescriptionCommandHandler(
        IAlgorithmDescriptionRepository algorithmDescriptionRepository)
        : IRequestHandler<UpdateAlgorithmDescriptionCommand>
    {
        public async Task Handle(UpdateAlgorithmDescriptionCommand request, CancellationToken cancellationToken)
        {
            var algorythmDescription = await algorithmDescriptionRepository.GetAsync()
                ?? throw new NotFoundException(
                    nameof(Domain.Entities.AlgorithmDescription),
                    Domain.Entities.AlgorithmDescription.SingletonId.ToString());

            algorythmDescription.Description = request.Description;
            algorythmDescription.LastUpdated = DateTime.UtcNow;
            await algorithmDescriptionRepository.SaveChangesAsync();
        }
    }
}

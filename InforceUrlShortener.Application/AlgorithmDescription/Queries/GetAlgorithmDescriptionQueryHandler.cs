using AutoMapper;
using InforceUrlShortener.Application.AlgorithmDescription.DTOs;
using InforceUrlShortener.Application.Helpers;
using InforceUrlShortener.Domain.Exceptions;
using InforceUrlShortener.Domain.RepositoriesInterfaces;
using MediatR;

namespace InforceUrlShortener.Application.AlgorithmDescription.Queries
{
    public class GetAlgorithmDescriptionQueryHandler(
        IAlgorithmDescriptionRepository algorithmDescriptionRepository,
        IMapper mapper)
        : IRequestHandler<GetAlgorithmDescriptionQuery, AlgorithmDescriptionDto>
    {
        public async Task<AlgorithmDescriptionDto> Handle(GetAlgorithmDescriptionQuery request, CancellationToken cancellationToken)
        {
            var algorythmDescription = await algorithmDescriptionRepository.GetAsync()
                ?? throw new NotFoundException(
                    nameof(Domain.Entities.AlgorithmDescription),
                    Domain.Entities.AlgorithmDescription.SingletonId.ToString());

            var algorythmDescriptionDto = mapper.Map<AlgorithmDescriptionDto>(algorythmDescription);
            algorythmDescriptionDto.LastUpdated = TimeZoneHelper.ConvertUtcToKyiv(algorythmDescription.LastUpdated);
            return algorythmDescriptionDto;
        }
    }
}

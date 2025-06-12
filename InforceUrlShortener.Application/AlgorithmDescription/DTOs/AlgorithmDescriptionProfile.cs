using AutoMapper;

namespace InforceUrlShortener.Application.AlgorithmDescription.DTOs
{
    public class AlgorithmDescriptionProfile : Profile
    {
        public AlgorithmDescriptionProfile()
        {
            CreateMap<Domain.Entities.AlgorithmDescription, AlgorithmDescriptionDto>();
        }
    }
}

using MediatR;

namespace InforceUrlShortener.Application.AlgorithmDescription.Commands
{
    public class UpdateAlgorithmDescriptionCommand : IRequest
    {
        public string Description { get; set; } = default!;
    }
}

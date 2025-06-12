using InforceUrlShortener.Application.AlgorithmDescription.Commands;
using InforceUrlShortener.Application.AlgorithmDescription.Queries;
using InforceUrlShortener.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceUrlShortener.API.Controllers
{
    [ApiController]
    [Route("algorithm-description")]
    [Authorize]
    public class AlgorithmDescriptionController(
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAlgorithmDescription()
        {
            var algorithmDescription = await mediator.Send(new GetAlgorithmDescriptionQuery());
            return Ok(algorithmDescription);
        }
        [HttpPatch]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UpdateAlgorithmDescription(UpdateAlgorithmDescriptionCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }
    }
}

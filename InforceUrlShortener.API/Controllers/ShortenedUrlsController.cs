using InforceUrlShortener.Application.ShortenedUrls.Commands.CreateShortenedUrl;
using InforceUrlShortener.Application.ShortenedUrls.Commands.DeleteShortenedUrl;
using InforceUrlShortener.Application.ShortenedUrls.Queries.GetPaginatedShortenedUrls;
using InforceUrlShortener.Application.ShortenedUrls.Queries.GetShortenedUrlById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceUrlShortener.API.Controllers
{
    [ApiController]
    [Route("api/shortenedUrl")]
    [Authorize]
    public class ShortenedUrlsController(
        IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateShortenedUrl(CreateShortenedUrlCommand command)
        {
            var shortenedUrlListItemDto = await mediator.Send(command);
            return Ok(shortenedUrlListItemDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShortenedUrlById(Guid id)
        {
            var shortenedUrl = await mediator.Send(new GetShortenedUrlByIdQuery() { Id = id });
            return Ok(shortenedUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetPaginatedShortenedUrls([FromQuery] GetPaginatedShortenedUrlsQuery query)
        {
            var shortenedUrls = await mediator.Send(query);
            return Ok(shortenedUrls);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShortenedUrl(Guid id)
        {
            await mediator.Send(new DeleteShortenedUrlCommand { Id = id });
            return NoContent();
        }
    }
}

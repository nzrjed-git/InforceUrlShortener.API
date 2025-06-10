using InforceUrlShortener.Application.ShortenedUrls.Queries.GetOriginalUrlByShortCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InforceUrlShortener.API.Controllers
{
    [ApiController]
    public class RedirectController(
        IMediator mediator): ControllerBase
    {
        [HttpGet("/{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var originalUrl = await mediator.Send(new GetOriginalUrlByShortCodeQuery { ShortCode = shortCode });

            return Redirect(originalUrl!);
        }
    }
}

using InforceUrlShortener.Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceUrlShortener.API.Controllers
{
    [ApiController]
    [Route("api/identity")]
    public class IdentityController(
        IMediator mediator) : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var userInfo = await mediator.Send(new GetUserInfoQuery());
            return Ok(userInfo);
        }
    }
}

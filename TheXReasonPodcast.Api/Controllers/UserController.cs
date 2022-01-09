using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheXReasonPodcast.Application.Interfaces;
using TheXReasonPodcast.Application.Models.Requests;
using TheXReasonPodcast.Application.Models.Responses;

namespace TheXReasonPodcast.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        [ProducesResponseType(typeof(AuthenticateResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Authenticate([FromBody] AuthenticateRequest AuthenticateRequest)
        {
            var response = _authenticationService.Authenticate(AuthenticateRequest);
            IActionResult result = response != null ? Ok(response) : Unauthorized();

            return result;
        }

        [HttpPut]    
        [AllowAnonymous]
        [Route("refreshCredentials")]
        [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult RefreshCredentials([FromBody] RefreshCredentialsRequest refreshRequest)
        {
            var response = _authenticationService.RefreshCredentials(refreshRequest);
            IActionResult result = response != null ? Ok(response) : Unauthorized();

            return result;
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheXReasonPodcast.Application.Interfaces;
using TheXReasonPodcast.Application.Models;

namespace TheXReasonPodcast.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJwtService _jwtAuthManager;

        public AccountController(IJwtService jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
        {
            return Ok(new { message = $"The auth works, username : {User.Identity.Name}" });
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Post([FromBody] LoginRequest loginRequest)
        {
            var token = _jwtAuthManager.Login(loginRequest.Username, loginRequest.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
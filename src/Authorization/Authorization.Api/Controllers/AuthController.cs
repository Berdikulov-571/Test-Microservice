using Authorization.Core.Models;
using Authorization.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Authorization.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtService;
     
        public AuthController(JWTService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse?>> Authenticate([FromForm] AuthenticationRequest authentication)
        {
            var result = await _jwtService.GenerateToken(authentication)!;

            if (result == null) return Unauthorized();

            return Ok(result);
        }
    }
}
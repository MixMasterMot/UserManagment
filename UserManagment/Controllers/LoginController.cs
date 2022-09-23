using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Models;
using UserManagment.Services;

namespace UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _authenticationService.Authenticate(model);
            if(response != null)
            {
                return Ok(response);
            }
            return BadRequest(new {message = "Username or password is incorrect"});
        }
    }
}

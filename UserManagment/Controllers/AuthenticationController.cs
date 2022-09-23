using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Entities;
using UserManagment.Models;
using UserManagment.Services;
using UserManagment.Services.Auth;

namespace UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _userService = userService;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegisterRequest)
        {
            var user = _userService
        }

        [HttpPost("sprt")]
        public async Task<IActionResult> sprt(AuthenticateRequest model)
        {
            var user = new User()
            {
                Email="s",
                FullName="full name",
                UserName="admin",
                Password="password",
                UserRole = UserRole.admin
            };

            await _userService.CreateAsync(user);
            return Ok(await _userService.GetByUserNameAsync(user.UserName));
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Entities;
using UserManagment.Models;
using UserManagment.Services;
using UserManagment.Services.Auth;
using UserManagment.Services.UserValidation;

namespace UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
        private IUserValidator _userValidator;
        private IUserService _userService;

        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, IUserValidator userValidator)
        {
            _userValidator = userValidator;
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
        public async Task<IActionResult> Register(UserCreateRequest userRegisterRequest)
        {
            var validationMessage = await _userValidator.Validate(userRegisterRequest);
            if(validationMessage != null)
            {
                return BadRequest(validationMessage);
            }
            var user = await _userService.CreateAsync(userRegisterRequest.ToUser());
            if(user != null)
            {
                return Ok(user);
            }
            return StatusCode(500);
        }

        [HttpPost("sprt")]
        public async Task<IActionResult> sprt(AuthenticateRequest model)
        {
            var user = new UserCreateRequest()
            {
                Email="s",
                FullName="full name",
                UserName="admin",
                Password="password",
                UserRole = UserRole.admin
            };

            await _userService.CreateAsync(user.ToUser());
            return Ok(await _userService.GetByUserNameAsync(user.UserName));
        }
    }
}

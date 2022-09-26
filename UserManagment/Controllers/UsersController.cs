using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserManagment.Entities;
using UserManagment.Helpers;
using UserManagment.Models;
using UserManagment.Services;
using UserManagment.Services.UserValidation;

namespace UserManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IUserValidator _userValidator;

        public UsersController(IUserService userService, IUserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers(int pageSize = 100, int page = 0)
        {
            return await _userService.GetAsync();
        }

        [Authorize]
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> GetByID(string id)
        {
            var user = await _userService.GetByIDAsync(id);
            if (user != null)
            {
                return user;
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet("{search}")]
        public async Task<ActionResult<List<User>>> Search(string search)
        {
            var t = await _userService.FindAsync(search);
            return t;
        }

        [Authorize]
        [HttpPatch("{id:length(24)}")]
        public async Task<ActionResult<User>> UpdateUser(string id, UserUpdateRequest user)
        {
            var validationMessage = await _userValidator.Validate(user);
            if (validationMessage != null)
            {
                return BadRequest(validationMessage);
            }

            var oldUser = await _userService.GetByIDAsync(id);
            if(oldUser is null)
            {
                return NotFound();
            }

            var newUser = user.ToUser();
            newUser.UserRole = user.UserRole ?? oldUser.UserRole;
            newUser.Password = user.Password ?? oldUser.Password;

            await _userService.UpdateAsync(newUser);
            return Ok(newUser);
        }

        [Authorize]
        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var oldUser = _userService.GetByIDAsync(id);
            if(oldUser is null)
            {
                return NotFound();
            }
            await _userService.RemoveAsync(id);
            return Ok();
        }
    }
}

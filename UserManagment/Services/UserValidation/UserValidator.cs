using Microsoft.IdentityModel.Tokens;
using UserManagment.Models;

namespace UserManagment.Services.UserValidation
{
    public class UserValidator: IUserValidator
    {
        private IUserService _userService;

        public UserValidator(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<string?> Validate(UserCreateRequest request)
        {
            if (request.UserName.IsNullOrEmpty()) return "Invalid Username";
            var user = await _userService.GetByUserNameAsync(request.UserName);
            if (user != null) return "Invalid Username";
            if (request.Password.IsNullOrEmpty()) return "Invalid password";
            if (request.Email.IsNullOrEmpty()) return "Invalid email";
            return null;
        }

        public async Task<string?> Validate(UserUpdateRequest request)
        {
            if (request.UserName.IsNullOrEmpty()) return "Invalid Username";
            var user = await _userService.GetByUserNameAsync(request.UserName);
            if (user != null) return "Invalid Username";
            if (request.Email.IsNullOrEmpty()) return "Invalid email";
            return null;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagment.Config;
using UserManagment.Entities;
using UserManagment.Models;

namespace UserManagment.Services.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(IUserService userService, IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request)
        {
            var user = await _userService.GetByUserNameAsync(request.Username);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) return null;

            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        private string GenerateJwtToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var claims = new[]
            {
                new Claim("id", user.Id),
                new Claim("userRoll", user.UserRole.ToString()),
            };

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(tokenDescripter);
            return handler.WriteToken(token);
        }
    }
}

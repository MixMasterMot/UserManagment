using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagment.Models;

namespace UserManagment.Services
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
            if(DecodePassword(user.Password) != request.Password) return null;

            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        private string DecodePassword(string password)
        {
            //TODO build decoder
            return password;
        }

        private string GenerateJwtToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = handler.CreateToken(tokenDescripter);
            return handler.WriteToken(token);
        }
    }
}

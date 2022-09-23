using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserManagment.Models;
using UserManagment.Services;

namespace UserManagment.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;
        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate requestDelegate, IOptions<JwtSettings> jwtSettings)
        {
            _nextRequestDelegate = requestDelegate;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token == null)
            {
                AttachUserToContext(context, userService, token);
            }

            await _nextRequestDelegate(context);
        }

        private async void AttachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // makes token expire at exact time
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                context.Items["User"] = await userService.GetByIDAsync(userId);
            }
            catch
            {
                // if we endup here the validation has failed
                // the request won't get to secure routes
            }
        }
    }
}

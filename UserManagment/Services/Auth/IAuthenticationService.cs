using UserManagment.Models;

namespace UserManagment.Services.Auth
{
    public interface IAuthenticationService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
    }
}

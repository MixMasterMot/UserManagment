using UserManagment.Models;

namespace UserManagment.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
    }
}

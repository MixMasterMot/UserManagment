using UserManagment.Models;

namespace UserManagment.Services.UserValidation
{
    public interface IUserValidator
    {
        Task<string?> Validate(UserCreateRequest request);
    }
}

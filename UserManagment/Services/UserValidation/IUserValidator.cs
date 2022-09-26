using UserManagment.Models;

namespace UserManagment.Services.UserValidation
{
    public interface IUserValidator
    {
        Task<string?> Validate(UserCreateRequest request);
        Task<string?> Validate(UserUpdateRequest request);
    }
}

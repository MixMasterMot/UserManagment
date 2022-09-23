using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagment.Models;

namespace UserManagment.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAsync();
        Task<User?> GetByIDAsync(string id);
        Task<User?> GetByUserNameAsync(string name);
        Task<List<User>> FindAsync(string term);
        Task CreateAsync(User newUser);
        Task UpdateAsync(string id, User updatedUser);
        Task RemoveAsync(string id);
    }
}

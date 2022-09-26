using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using UserManagment.Entities;

namespace UserManagment.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAsync(int pageSize = 100, int page = 0);
        Task<User?> GetByIDAsync(string id);
        Task<User?> GetByUserNameAsync(string name);
        Task<List<User>> FindAsync(string term, int pageSize = 100, int page = 0);
        Task<User?> CreateAsync(User newUser);
        Task UpdateAsync(User updatedUser);
        Task RemoveAsync(string id);
    }
}

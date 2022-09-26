using System.ComponentModel.DataAnnotations;
using UserManagment.Entities;

namespace UserManagment.Models
{
    public class UserUpdateRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Password { get; set; }

        public UserRole? UserRole { get; set; }

        public User ToUser()
        {
            return new User
            {
                Id = Id,
                Email = Email,
                UserName = UserName,
                UserRole = UserRole,
                FullName = FullName,
                Password = Password != null ? BCrypt.Net.BCrypt.HashPassword(Password) : null
            };
        }
    }
}

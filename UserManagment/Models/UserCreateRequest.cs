using System.ComponentModel.DataAnnotations;
using UserManagment.Entities;

namespace UserManagment.Models
{
    public class UserCreateRequest
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRole UserRole { get; set; } = UserRole.viewer;

        public User ToUser()
        {
            return new User { 
                Email = Email, 
                UserName = UserName, 
                UserRole = UserRole, 
                FullName = FullName, 
                Password = BCrypt.Net.BCrypt.HashPassword(Password)
            };
        }
    }
}

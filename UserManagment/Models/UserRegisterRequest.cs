using System.ComponentModel.DataAnnotations;
using UserManagment.Entities;

namespace UserManagment.Models
{
    public class UserRegisterRequest
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
    }
}

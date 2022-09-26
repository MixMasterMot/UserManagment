using System.ComponentModel.DataAnnotations;

namespace UserManagment.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        //TODO
        // The password is not currently encripted when passed over the network
        [Required]
        public string Password { get; set; }
    }
}

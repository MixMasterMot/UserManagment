using UserManagment.Entities;

namespace UserManagment.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public UserRole UserRole { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse( User user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            Token = token;
            UserRole = user.UserRole ?? UserRole.viewer;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ArtClub.DataAccess.Model
{
    public class User : ModelEntity
    {

        public string Role { get; set; }   = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


    }
}

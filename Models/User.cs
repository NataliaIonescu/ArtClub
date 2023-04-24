using System.ComponentModel.DataAnnotations;

namespace ArtClub.Models
{
    public class User : ModelEntity
    {

        public string? Role { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<Event>? Events { get; set; } // un user poate crea mai multe evenimente
    }
}

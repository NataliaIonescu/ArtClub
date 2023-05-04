

namespace ArtClub.Models
{
    public class Event : ModelEntity
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

  

        public ICollection<Resource>? Resources { get; set; }
        public ICollection<Invitation>? Invitations { get; set; }
    }
}

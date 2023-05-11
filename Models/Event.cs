
namespace ArtClub.Models
{
    public class Event : ModelEntity
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public int ResourceId { get; set; }
        public Resource? Resource { get; set; }
        
    }
}

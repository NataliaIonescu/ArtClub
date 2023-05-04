namespace ArtClub.Models
{
    public class Invitation : ModelEntity
    {
        public int EventID { get; set; } // Required foreign key property
        public Event? Event { get; set; } // o inv este create pentru un event
    }
}

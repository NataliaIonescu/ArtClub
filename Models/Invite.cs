using System.ComponentModel.DataAnnotations;


namespace ArtClub.Models
    {
        public class Invite: ModelEntity
        {

            [Required]
            [EmailAddress]
            public string? EmailForReceiver { get; set; }
            public string? RecipientId { get; internal set; }
            public string? SenderEmail { get; internal set; }
            public int EventId { get; set; }

            public Event ?Event { get; set; }
 
    }
    }
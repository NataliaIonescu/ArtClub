
using System.ComponentModel.DataAnnotations;

namespace ArtClub.Models
{
    public class Resource : ModelEntity
    {

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
       // public bool Availability { get; set; } = true;
        public decimal Price { get; set; }

        public  Event? Event { get; set; }
    }
}

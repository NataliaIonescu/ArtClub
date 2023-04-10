
namespace ArtClub.DataAccess.Model
{
    public class Event : ModelEntity
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string Creator { get; set; } = string.Empty;

        public ICollection<Resource>? Resources { get; set; }
        public ICollection<User>? Users { get; set; }

    }
}

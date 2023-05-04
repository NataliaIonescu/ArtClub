﻿namespace ArtClub.Models
{
    public class Event : ModelEntity
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string Creator { get; set; } = string.Empty;

        public ICollection<Resource>? Resources { get; set; }
        public ICollection<Invitation>? Invitations { get; set; }
        public User? User { get; set; } // un event poate fi creat de un user
    }
}

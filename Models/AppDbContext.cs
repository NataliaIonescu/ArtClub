using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ArtClub.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
         public DbSet<User> Users { get; set; }
        //public override DbSet<IdentityUser> Users { get; set; }
        //public DbSet<User> CustomUsers { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Payment> Payments { get; set; }

    
    }

}

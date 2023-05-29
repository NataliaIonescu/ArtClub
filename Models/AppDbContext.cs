
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace ArtClub.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }
       
        //   public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invite> Invites { get; set; } = default!;
    }
}

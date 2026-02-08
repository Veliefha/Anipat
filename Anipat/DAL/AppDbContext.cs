using Anipat.Models;
using Microsoft.EntityFrameworkCore;

namespace Anipat.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
           
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


    }
}

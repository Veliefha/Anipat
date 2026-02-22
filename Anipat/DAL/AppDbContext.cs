using Anipat.Models;
using Microsoft.EntityFrameworkCore;

namespace Anipat.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Pet> Pets { get; set; }
    }
}
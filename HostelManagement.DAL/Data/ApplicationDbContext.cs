using HostelManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace HostelManagement.DAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Payment>().Property(o => o.Amount).HasPrecision(15,2);
        }

        public DbSet<Hostel> Hostel_Details { get; set; }

        public DbSet<Room> Rooms { get; set; }
        
        public DbSet<Student> Students { get; set; }

        public DbSet<Meal> Meal_Details { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        
        public DbSet<Payment> Payments { get; set; }

    }
}

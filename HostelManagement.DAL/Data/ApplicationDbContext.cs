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

            modelBuilder.Entity<Room>()
               .HasOne(h => h.hostels)
               .WithMany(r => r.rooms)
               .HasForeignKey(h => h.HostelId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Student>()
               .HasOne(r => r.rooms)
               .WithMany(s => s.students)
               .HasForeignKey(r => r.RoomId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Meal>()
               .HasOne(s => s.students)
               .WithMany(m => m.meals)
               .HasForeignKey(s => s.StudentId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
               .HasOne(s => s.students)
               .WithMany(b => b.bookings)
               .HasForeignKey(s => s.StudentId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
               .HasOne(b => b.bookings)
               .WithMany(p => p.payments)
               .HasForeignKey(b => b.BookingId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>().Property(o => o.Amount).HasPrecision(15,2);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Hostel> Hostels { get; set; }

        public DbSet<Room> Rooms { get; set; }
        
        public DbSet<Student> Students { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        
        public DbSet<Payment> Payments { get; set; }

    }
}

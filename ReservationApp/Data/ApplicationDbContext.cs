using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReservationApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);

    //     // Fluent API configurations
    //     modelBuilder.Entity<Reservation>()
    //         .HasKey(r => r.ReservationId);

    //     modelBuilder.Entity<Reservation>()
    //         .Property(r => r.RoomId)
    //         .HasColumnName("RoomId");

    //     modelBuilder.Entity<Reservation>()
    //         .HasOne(r => r.Room)
    //         .WithMany()
    //         .HasForeignKey(r => r.RoomId)
    //         .OnDelete(DeleteBehavior.Cascade);

    //     modelBuilder.Entity<Room>()
    //         .HasKey(r => r.RoomId);
    // }
}
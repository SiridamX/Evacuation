using Microsoft.EntityFrameworkCore;
using Evacuation.Domain.Entities;

namespace Evacuation.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Zone> Zones { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleAssignment> Assignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Zone>(entity =>
            {
                entity.HasKey(z => z.ZoneId);
                entity.Ignore(z => z.Id);

                entity.Property(z => z.ZoneId)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(z => z.UrgencyLevel)
                      .IsRequired();
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.VehicleId);
                entity.Ignore(v => v.Id); 

                entity.Property(v => v.VehicleId)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(v => v.Type)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<VehicleAssignment>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.ZoneId)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(a => a.VehicleId)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasOne(a => a.Zone)
                      .WithMany(z => z.Assignments)
                      .HasForeignKey(a => a.ZoneId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.Vehicle)
                      .WithMany()
                      .HasForeignKey(a => a.VehicleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
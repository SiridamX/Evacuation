using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evacuation.Domain.Entities;

public class VehicleAssignmentConfiguration : IEntityTypeConfiguration<VehicleAssignment>
{
    public void Configure(EntityTypeBuilder<VehicleAssignment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.VehicleId)
            .IsRequired();

        builder.Property(a => a.ZoneId)
            .IsRequired();

        builder.Property(a => a.AssignedPeople)
            .IsRequired();

        builder.Property(a => a.EstimatedArrival)
            .IsRequired();

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(a => a.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Zone>()
            .WithMany()
            .HasForeignKey(a => a.ZoneId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
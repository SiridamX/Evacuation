using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evacuation.Domain.Entities;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.VehicleId)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.Capacity)
            .IsRequired();

        builder.Property(v => v.Speed)
            .IsRequired();

        builder.Property(v => v.Latitude)
            .IsRequired();

        builder.Property(v => v.Longitude)
            .IsRequired();

        builder.Property(v => v.IsAvailable)
            .HasDefaultValue(true);
    }
}
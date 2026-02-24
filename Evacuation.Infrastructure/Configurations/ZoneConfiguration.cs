using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Evacuation.Domain.Entities;

public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
{
    public void Configure(EntityTypeBuilder<Zone> builder)
    {
        builder.HasKey(z => z.Id);

        builder.Property(z => z.ZoneId)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(z => z.ZoneId)
            .IsUnique();

        builder.Property(z => z.Latitude)
            .IsRequired();

        builder.Property(z => z.Longitude)
            .IsRequired();

        builder.Property(z => z.NumberOfPeople)
            .IsRequired();

        builder.Property(z => z.EvacuatedPeople)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(z => z.UrgencyLevel)
            .IsRequired();

    }
}
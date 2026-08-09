using DirectoryService.Domain.LocationManagement.Aggregate;
using DirectoryService.Domain.LocationManagement.Ids;
using DirectoryService.Domain.Shar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("location");

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasConversion(id => id!.Value, value => LocationId.Create(value))
            .HasColumnName("id");

        builder.ComplexProperty(l => l.LocationName, lnb =>
        {
            lnb.Property(ln => ln.Value)
                .HasColumnName("location_name")
                .HasMaxLength(LengthConstants.Length120)
                .IsRequired();
        });

        builder.ComplexProperty(l => l.Address, ab =>
        {
            ab.Property(a => a.Country)
                .HasColumnName("country")
                .HasMaxLength(LengthConstants.Length100)
                .IsRequired();
            ab.Property(a => a.Region)
                .HasColumnName("region")
                .HasMaxLength(LengthConstants.Length100)
                .IsRequired();
            ab.Property(a => a.City)
                .HasColumnName("city")
                .HasMaxLength(LengthConstants.Length100)
                .IsRequired();
            ab.Property(a => a.Street)
                .HasColumnName("street")
                .HasMaxLength(LengthConstants.Length250)
                .IsRequired();
            ab.Property(a => a.HouseNumber)
                .HasColumnName("house_number")
                .HasMaxLength(LengthConstants.Length20)
                .IsRequired();
        });

        builder.Property(l => l.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(l => l.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();
    }
}
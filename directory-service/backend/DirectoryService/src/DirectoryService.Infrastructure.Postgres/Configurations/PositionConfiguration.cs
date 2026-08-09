using DirectoryService.Domain.PositionManagement.Aggregate;
using DirectoryService.Domain.PositionManagement.Ids;
using DirectoryService.Domain.Shar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("position");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id!.Value, value => PositionId.Create(value))
            .HasColumnName("id");

        builder.ComplexProperty(p => p.PositionName, pnb =>
        {
            pnb.Property(pn => pn.Value)
                .HasColumnName("position_name")
                .HasMaxLength(LengthConstants.Length100)
                .IsRequired();
        });
    }
}
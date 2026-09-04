using DirectoryService.Domain.Departments.Aggregate;
using DirectoryService.Domain.Departments.Ids;
using DirectoryService.Domain.Shar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Id)
            .HasConversion(id => id!.Value, value => DepartmentId.Create(value))
            .ValueGeneratedNever()
            .HasColumnName("id");

        builder.ComplexProperty(d => d.DepartmentName, dnb =>
        {
            dnb.Property(dn => dn.Value)
                .HasColumnName("department_name")
                .HasMaxLength(LengthConstants.Length150)
                .IsRequired();
        });

        builder.OwnsOne(d => d.Slug, slugBuilder =>
        {
            slugBuilder.Property(i => i.Value)
                .HasColumnName("slug")
                .HasMaxLength(LengthConstants.Length150)
                .IsRequired();
            slugBuilder.HasIndex(i => i.Value).IsUnique();
        });

        builder.ComplexProperty(d => d.Path, pb =>
        {
            pb.Property(p => p.Value)
                .HasColumnName("path")
                .HasMaxLength(LengthConstants.Length1000)
                .IsRequired();
        });

        builder.Property(d => d.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(d => d.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.Property(d => d.ParentId)
            .HasConversion(
                id => id != null ? id.Value : (Guid?)null,
                value => value.HasValue ? DepartmentId.Create(value.Value) : null)
            .HasColumnName("parent_id")
            .IsRequired(false);

        builder.HasOne(d => d.Parent)
            .WithMany()
            .HasForeignKey(d => d.ParentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
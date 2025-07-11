using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Contexts.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("StudentId");
        builder.Property(t => t.Email).HasMaxLength(255);
        builder.Property(t => t.Name).HasMaxLength(255);
        builder.HasIndex(t => t.Email).IsUnique();
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Contexts.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).HasColumnName("CourseId");
        builder.Property(t => t.CourseName).HasMaxLength(255);
        builder.HasIndex(t => t.CourseName).IsUnique();
        builder.HasIndex(t => t.ClassroomNumber).IsUnique();
    }
    
}
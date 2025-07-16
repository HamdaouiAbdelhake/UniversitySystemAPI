using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using University.Data.Contexts.Configurations;
using University.Data.Entities;
using University.Data.Entities.Identity;

namespace University.Data.Contexts;

public class UniversityDbContext : IdentityDbContext<
    User,
    IdentityRole<int>,
    int
>
{
    //To add the sql server from the appsettings.json file in program.cs
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }
    
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserClaimConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
    }
}
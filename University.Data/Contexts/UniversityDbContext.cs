using Microsoft.EntityFrameworkCore;
using University.Data.Contexts.Configurations;
using University.Data.Entities;

namespace University.Data.Contexts;

public class UniversityDbContext : DbContext
{
    //To add the sql server from the appsettings.json file in program.cs
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }
    
    
    public DbSet<Student> Students { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
    }
}
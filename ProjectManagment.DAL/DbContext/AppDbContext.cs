using Microsoft.EntityFrameworkCore;
using ProjectManagment.DAL.Context.Configurations;
using ProjectManagment.DAL.Entities;

namespace ProjectManagment.DAL.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set;}
    public DbSet<Project> Projects { get; set;}
    public DbSet<Company> Companies { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
    }
}

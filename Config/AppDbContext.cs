using EmptyBlazorApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace EmptyBlazorApp1.Config;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } 
    public DbSet<InternshipDirection> InternshipDirections { get; set; } 
    public DbSet<CurrentProject> CurrentProjects { get; set; } 
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        Database.EnsureCreated(); 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InternshipDirection>()
            .HasIndex(u => u.Name)
            .IsUnique(); 
        
        modelBuilder.Entity<CurrentProject>()
            .HasIndex(u => u.Name)
            .IsUnique(); 
    }
}
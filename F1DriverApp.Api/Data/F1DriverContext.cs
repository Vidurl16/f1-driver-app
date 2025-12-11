using F1DriverApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace F1DriverApp.Api.Data;

public class F1DriverContext : DbContext
{
    public F1DriverContext(DbContextOptions<F1DriverContext> options) : base(options)
    {
    }
    
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Team> Teams { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure Team entity
        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(t => t.TeamId);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
        });
        
        // Configure Driver entity
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(d => d.DriverId);
            entity.Property(d => d.Name).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Team).IsRequired().HasMaxLength(100);
            entity.Property(d => d.Country).IsRequired().HasMaxLength(100);
            entity.Property(d => d.ChampionshipPoints).IsRequired();
            
            // Configure relationship
            entity.HasOne(d => d.TeamEntity)
                  .WithMany(t => t.Drivers)
                  .HasForeignKey(d => d.TeamId)
                  .OnDelete(DeleteBehavior.SetNull);
        });
    }
}

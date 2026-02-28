using TestTask.Entities;
using TestTask.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Persistence.Postgres;

public class AppDbContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Key> Keys { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
using Entities;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Postgres;

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
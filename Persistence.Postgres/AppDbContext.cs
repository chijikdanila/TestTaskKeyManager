using Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Postgres;

public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(200);
        });

        modelBuilder.Entity<Key>(entity =>
        {
            entity.HasKey(k => k.Id);
            entity.Property(k => k.KeyValue)
                .IsRequired();
            entity.Property(k => k.StartedAt)
                .IsRequired();
            entity.Property(k => k.EndedAt)
                .IsRequired();
            entity.Property(k => k.KeyBlockStatus)
                .IsRequired();
            entity.HasOne(k => k.Organization)
                .WithMany(o => o.KeysInfo)
                .HasForeignKey(k => k.OrganizationId);

        });
    }
}
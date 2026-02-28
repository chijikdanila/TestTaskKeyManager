using TestTask.Entities;
using TestTask.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestTask.Persistence.Postgres.Configurations;

public class KeysConfiguration : IEntityTypeConfiguration<Key>
{
    public void Configure(EntityTypeBuilder<Key> builder)
    {
        builder
            .HasKey(k => k.Id);
        
        builder
            .Property(k => k.KeyValue)
            .IsRequired();
        
        builder
            .Property(k => k.StartedAt)
            .IsRequired();
        
        builder
            .Property(k => k.EndedAt)
            .IsRequired();
        
        builder
            .Property(k => k.KeyStatus)
            .IsRequired();
        
        builder
            .HasOne(k => k.Organization)
            .WithMany(o => o.KeysInfo)
            .HasForeignKey(k => k.OrganizationId);
        
        builder
            .HasIndex(k => k.KeyValue)
            .IsUnique();
    }
}
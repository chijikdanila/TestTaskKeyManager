using TestTask.Entities;
using TestTask.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestTask.Persistence.Postgres.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder
            .HasKey(o => o.Id);
        
        builder
            .Property(o => o.Name)
            .IsRequired()
            .HasMaxLength(200);
        
        builder
            .HasIndex(o => o.Name)
            .IsUnique();
    }
}
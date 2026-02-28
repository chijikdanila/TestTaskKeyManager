using BLL.Services;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Postgres.Seeders;

public class OrganizationDbSeeder : IDbSeeder
{
    private readonly AppDbContext _context;
    
    public int Order { get; }

    public OrganizationDbSeeder(AppDbContext context)
    {
        _context = context;
        Order = 1;
    }

    public async Task SeedAsync()
    {
        if (await _context.Organizations.AnyAsync())
        {
            return;
        }

        _context.Organizations.Add(Organization.Create("ООО \"Организация\""));
        _context.Organizations.Add(Organization.Create("ОАО \"Организация\""));
        _context.Organizations.Add(Organization.Create("ЗАО \"Организация\""));
        
        await _context.SaveChangesAsync();
    }
}
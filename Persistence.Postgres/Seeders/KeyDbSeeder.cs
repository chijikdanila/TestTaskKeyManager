using BLL.Services;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Primitives.Enums;

namespace Persistence.Postgres.Seeders;

public class KeyDbSeeder : IDbSeeder
{
    private readonly AppDbContext _context;
    
    public int Order { get; }

    public KeyDbSeeder(AppDbContext context)
    {
        _context = context;
        Order = 2;
    }

    public async Task SeedAsync()
    {
        if (await _context.Keys.AnyAsync())
        {
            return;
        }

        if (!await _context.Organizations.AnyAsync(x => x.Name == "Тестовая организация"))
        {
            _context.Organizations.Add(Organization.Create("Тестовая организация"));
            await _context.SaveChangesAsync();
        }

        var organization = await _context.Organizations
            .FirstAsync(x => x.Name == "Тестовая организация");
        
        _context.Keys.Add(Key.Create(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(7), organization.Id, KeyStatus.Active));
        _context.Keys.Add(Key.Create(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(14), organization.Id, KeyStatus.Active));
        _context.Keys.Add(Key.Create(Guid.NewGuid(), DateTime.UtcNow, DateTime.UtcNow.AddDays(21), organization.Id, KeyStatus.Blocked));
        
        await _context.SaveChangesAsync();
    }
}
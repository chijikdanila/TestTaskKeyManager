using TestTask.BLL.Services;
using TestTask.Entities;
using TestTask.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Persistence.Postgres.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;
    
    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<long> Create(Organization organization)
    {
        _context.Add(organization);
        await _context.SaveChangesAsync();
        _context.Entry(organization).State = EntityState.Detached;
        
        return organization.Id;
    }

    public async Task Update(Organization organization)
    {
        _context.Update(organization);
        await _context.SaveChangesAsync();
        _context.Entry(organization).State = EntityState.Detached;
    }

    public async Task Delete(long id)
    {
        await _context.Organizations
            .Where(o => o.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task<Organization> GetById(long id)
    {
        var organization = await _context.Organizations
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == id);

        if (organization is null)
        {
            throw new Exception($"Орагнизация с id {id} не найдена");
        }

        return organization;
    }

    public async Task<List<Organization>> GetAll()
    {
        return await _context.Organizations
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Organization>> GetAllWithKeys()
    {
        return await _context.Organizations
            .AsNoTracking()
            .Include(x => x.KeysInfo)
            .ToListAsync();
    }
}
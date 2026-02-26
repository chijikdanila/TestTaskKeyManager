using Entities;

namespace Persistence.Postgres.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;
    
    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task Add(Organization organization)
    {
        throw new NotImplementedException();
    }

    public Task Update(Organization organization)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Organization> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Organization>> GetAll()
    {
        throw new NotImplementedException();
    }
}
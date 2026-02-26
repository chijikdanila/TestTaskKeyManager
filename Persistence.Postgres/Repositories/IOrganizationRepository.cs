using Entities;

namespace Persistence.Postgres.Repositories;

public interface IOrganizationRepository
{
    public Task Add(Organization organization);
    public Task Update(Organization organization);
    public Task Delete(long id);
    public Task<Organization> GetById(long id);
    public Task<List<Organization>> GetAll();
}
using Entities;

namespace Persistence.Postgres.Repositories;

public interface IKeyRepository
{
    public Task Add(Key key);
    public Task Update(Key key);
    public Task Delete(long id);
    public Task<Key> GetById(long id);
    public Task<List<Key>> GetByOrganizationId(long id);
    public Task<List<Key>> GetAll();
}
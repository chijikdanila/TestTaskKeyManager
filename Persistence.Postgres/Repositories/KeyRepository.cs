using Entities;

namespace Persistence.Postgres.Repositories;

public class KeyRepository :IKeyRepository
{
    public Task Add(Key key)
    {
        throw new NotImplementedException();
    }

    public Task Update(Key key)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Key> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Key>> GetByOrganizationId(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Key>> GetAll()
    {
        throw new NotImplementedException();
    }
}
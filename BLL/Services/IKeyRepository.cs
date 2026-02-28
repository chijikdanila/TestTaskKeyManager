using Contracts.Dtos;
using Entities.Entities;
using Primitives.Enums;

namespace BLL.Services;

public interface IKeyRepository
{
    Task<long> Create(Key key);
    Task Update(Key key);
    Task Delete(long id);
    Task<Key> GetById(long id);
    Task<List<Key>> GetAll();
}
using Contracts;

namespace BLL.Services;

public interface IKeyService
{
    public Task<KeyDetailsDto> Create(KeyCreateDto dto);
    public Task Update(long id, KeyCreateDto dto);
    public Task Delete(long id);
    public Task SetBlock(long id, bool isBlocked);
    public Task<KeyDetailsDto> GetById(long id);
    public Task<List<KeyDetailsDto>> GetByOrganizationId(long id);
    public Task<List<KeyDetailsDto>> GetAll();
}
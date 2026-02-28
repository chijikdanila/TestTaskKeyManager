using BLL.Services;
using Contracts.Dtos;
using Entities.Entities;

namespace Services.Services;

public class KeysesDataAccessService : IKeysService
{
    private readonly IKeyRepository  _repository;

    public KeysesDataAccessService(IKeyRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> Create(KeyCreateDto dto)
    {
        return await _repository.Create(Key.Create(dto.KeyValue, dto.StartedAt, dto.EndedAt, dto.OrganizationId, dto.KeyStatus));
    }

    public async Task Update(KeyDetailsDto dto)
    {
        var key = Key.Create(dto.KeyValue, dto.StartedAt, dto.EndedAt, dto.OrganizationId, dto.KeyStatus);
        key.Id = dto.Id;
        await _repository.Update(key);
    }

    public async Task Delete(long id)
    {
        await _repository.Delete(id);
    }

    public async Task<KeyDetailsDto> GetById(long id)
    {
        var key = await _repository.GetById(id);
        
        return new KeyDetailsDto(key.Id, key.KeyValue, key.StartedAt, key.EndedAt, key.OrganizationId, key.Organization.Name, key.KeyStatus);
    }

    public async Task<List<KeyDetailsDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(x => new KeyDetailsDto(x.Id, x.KeyValue, x.StartedAt, x.EndedAt, x.OrganizationId, x.Organization.Name, x.KeyStatus)).ToList();
    }
}
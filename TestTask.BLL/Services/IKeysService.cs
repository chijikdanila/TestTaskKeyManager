using TestTask.Contracts.Dtos;

namespace TestTask.BLL.Services;

public interface IKeysService
{
    Task<long> Create(KeyCreateDto dto);
    Task Update(KeyDetailsDto dto);
    Task Delete(long id);
    Task<KeyDetailsDto> GetById(long id);
    Task<List<KeyDetailsDto>> GetAll();
}
using Contracts;

namespace BLL.Services;

public interface IOrganizationService
{
    public Task<OrganizationCreateDto> Create(OrganizationCreateDto organization);
    public Task Update(long id, OrganizationCreateDto organization);
    public Task Delete(long id);
    public Task<OrganizationDetailsDto> GetById(long id);
    public Task<List<OrganizationDetailsDto>> GetAll();
}
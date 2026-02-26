using BLL.Services;
using Contracts;

namespace Services;

public class OrganizationDataAccessService : IOrganizationService
{
    private readonly IOrganizationService _service;
    
    public OrganizationDataAccessService(IOrganizationService service)
    {
        _service = service;
    }
    
    public Task<OrganizationCreateDto> Create(OrganizationCreateDto organization)
    {
        throw new NotImplementedException();
    }

    public Task Update(long id, OrganizationCreateDto organization)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Task<OrganizationDetailsDto> GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrganizationDetailsDto>> GetAll()
    {
        throw new NotImplementedException();
    }
}
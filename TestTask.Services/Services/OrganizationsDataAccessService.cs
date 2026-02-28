using TestTask.BLL.Services;
using TestTask.Contracts.Dtos;
using TestTask.Entities.Entities;

namespace TestTask.Services.Services;

public class OrganizationsDataAccessService : IOrganizationsService
{
    private readonly IOrganizationRepository _repository;

    public OrganizationsDataAccessService(IOrganizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> Create(OrganizationCreateDto organization)
    {
        return await _repository.Create(Organization.Create(organization.Name));
    }

    public async Task Update(OrganizationDetailsDto organization)
    {
        var organizationEntity = Organization.Create(organization.Name);
        organizationEntity.Id = organization.Id;
        await _repository.Update(organizationEntity);
    }

    public async Task Delete(long id)
    {
        await _repository.Delete(id);
    }

    public async Task<OrganizationDetailsDto> GetById(long id)
    {
        var organization = await _repository.GetById(id);
        
        return new OrganizationDetailsDto(organization.Id, organization.Name);
    }

    public async Task<List<OrganizationDetailsDto>> GetAll()
    {
        return (await _repository.GetAll()).Select(x => new OrganizationDetailsDto(x.Id, x.Name)).ToList();
    }

    public async Task<List<Organization>> GetAllEntitiesWithKeys()
    {
        return await _repository.GetAllWithKeys();
    }
}
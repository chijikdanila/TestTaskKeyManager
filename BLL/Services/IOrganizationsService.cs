using Contracts;
using Contracts.Dtos;
using Entities.Entities;

namespace BLL.Services;

public interface IOrganizationsService
{
    Task<long> Create(OrganizationCreateDto organization);
    Task Update(OrganizationDetailsDto organization);
    Task Delete(long id);
    Task<OrganizationDetailsDto> GetById(long id);
    Task<List<OrganizationDetailsDto>> GetAll();
    Task<List<Organization>> GetAllEntitiesWithKeys();
}
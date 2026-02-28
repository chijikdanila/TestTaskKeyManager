using Entities;
using Entities.Entities;

namespace BLL.Services;

public interface IOrganizationRepository
{
    Task<long> Create(Organization organization);
    Task Update(Organization organization);
    Task Delete(long id);
    Task<Organization> GetById(long id);
    Task<List<Organization>> GetAll();
    Task<List<Organization>> GetAllWithKeys();
}
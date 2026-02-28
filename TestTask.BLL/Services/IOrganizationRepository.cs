using TestTask.Entities;
using TestTask.Entities.Entities;

namespace TestTask.BLL.Services;

public interface IOrganizationRepository
{
    Task<long> Create(Organization organization);
    Task Update(Organization organization);
    Task Delete(long id);
    Task<Organization> GetById(long id);
    Task<List<Organization>> GetAll();
    Task<List<Organization>> GetAllWithKeys();
}
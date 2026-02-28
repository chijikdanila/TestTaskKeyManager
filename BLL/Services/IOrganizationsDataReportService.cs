using Entities.Entities;

namespace BLL.Services;

public interface IOrganizationsDataReportService
{
    void SaveOrganizationsReport(List<Organization> organizations, string filePath);
}
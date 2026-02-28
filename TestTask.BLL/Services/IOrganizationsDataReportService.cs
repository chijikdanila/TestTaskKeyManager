using TestTask.Entities.Entities;

namespace TestTask.BLL.Services;

public interface IOrganizationsDataReportService
{
    void SaveOrganizationsReport(List<Organization> organizations, string filePath);
}
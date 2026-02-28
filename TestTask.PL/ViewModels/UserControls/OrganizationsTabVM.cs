using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using TestTask.BLL.Services;
using TestTask.Contracts.Dtos;
using TestTask.PL.ViewModels.Windows;
using TestTask.PL.Views.Windows;
using TestTask.Utilities.Commands;

namespace TestTask.PL.ViewModels.UserControls;

public class OrganizationsTabVM : BaseTabVM
{
    private readonly IOrganizationsService _organizationsService;
    private readonly IOrganizationsDataReportService _organizationsDataReportService;
    
    private List<OrganizationDetailsDto>  _organizations;
    
    private string _filter;
    public string Filter
    {
        get => _filter;
        set
        {
            SetField(ref _filter, value);
            ApplyFilter();
        }
    }
    
    private ICollectionView _filteredOrganizations;
    public ICollectionView FilteredOrganizations
    {
        get => _filteredOrganizations;
        set => SetField(ref _filteredOrganizations, value);
    }
    
    public ICommand<OrganizationDetailsDto?> OpenSaveOrganizationWindowCommand { get; }
    public ICommand<OrganizationDetailsDto> DeleteOrganizationCommand { get; }
    public ICommand RefreshCommand { get; }
    public ICommand SaveReportCommand { get; }

    public OrganizationsTabVM(
        IOrganizationsService organizationsService,
        IOrganizationsDataReportService organizationsDataReportService)
    {
        _organizationsService = organizationsService;
        _organizationsDataReportService = organizationsDataReportService;

        OpenSaveOrganizationWindowCommand = ICommand<OrganizationDetailsDto?>.From(OpenSaveOrganizationWindow);
        DeleteOrganizationCommand = ICommand<OrganizationDetailsDto>.From(DeleteOrganization);
        RefreshCommand = ICommand.From(Refresh);
        SaveReportCommand = ICommand.From(SaveReport);
        Filter = string.Empty;
    }

    private void OpenSaveOrganizationWindow(OrganizationDetailsDto? organization)
    {
        var vm = new SaveOrganizationWindowVM(_organizationsService, organization);
        var window = new SaveOrganizationWindow(vm);
        window.ShowDialog();
        RefreshCommand.Execute();
    }

    private async Task DeleteOrganization(OrganizationDetailsDto organization)
    {
        try
        {
            await _organizationsService.Delete(organization.Id);
            _organizations.Remove(organization);
            FilteredOrganizations.Refresh();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    protected override async Task Refresh()
    {
        try
        {
            _organizations = [.. await _organizationsService.GetAll()];
            FilteredOrganizations = CollectionViewSource.GetDefaultView(_organizations);
            FilteredOrganizations.Filter = SetFilter;
            FilteredOrganizations.SortDescriptions.Add(new SortDescription(nameof(OrganizationDetailsDto.Id), ListSortDirection.Ascending));
            FilteredOrganizations.Refresh();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private bool SetFilter(object item)
    {
        if (string.IsNullOrWhiteSpace(Filter))
        {
            return true;
        }

        var organization = item as OrganizationDetailsDto;
        
        return organization?.Name.Contains(Filter, StringComparison.InvariantCultureIgnoreCase) == true;
    }

    private void ApplyFilter()
    {
        FilteredOrganizations?.Refresh();
    }

    private async Task SaveReport()
    {
        try
        {
            var organizations = await _organizationsService.GetAllEntitiesWithKeys();
            
            _organizationsDataReportService.SaveOrganizationsReport(organizations, $"{DateTime.Now:yy_MM_dd_HH_mm_ss}");
            
            MessageBox.Show("Отчет успешно создан");
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
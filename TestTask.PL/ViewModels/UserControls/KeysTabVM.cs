using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using TestTask.BLL.Services;
using TestTask.Contracts.Dtos;
using TestTask.PL.ViewModels.Windows;
using TestTask.PL.Views.Windows;
using TestTask.Utilities.Commands;

namespace TestTask.PL.ViewModels.UserControls;

public class KeysTabVM : BaseTabVM
{
    private readonly IOrganizationsService _organizationsService;
    private readonly IKeysService _keysService;
    
    private List<KeyDetailsDto>  _keys;
    
    private ObservableCollection<OrganizationDetailsDto>  _organizations;
    public ObservableCollection<OrganizationDetailsDto> Organizations
    {
        get => _organizations;
        set => SetField(ref _organizations, value);
    }
    
    private OrganizationDetailsDto _selectedOrganization;
    public OrganizationDetailsDto SelectedOrganization
    {
        get => _selectedOrganization;
        set
        {
            SetField(ref _selectedOrganization, value);
            ApplyFilter();
        }
    }
    
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
    
    private ICollectionView _filteredKeys;
    public ICollectionView FilteredKeys
    {
        get => _filteredKeys;
        set => SetField(ref _filteredKeys, value);
    }
    
    public ICommand<KeyDetailsDto?> OpenSaveKeyWindowCommand { get; }
    public ICommand<KeyDetailsDto> DeleteKeyCommand { get; }
    public ICommand RefreshCommand { get; }

    public KeysTabVM(
        IKeysService keysService,
        IOrganizationsService organizationsService)
    {
        _keysService = keysService;
        _organizationsService = organizationsService;

        OpenSaveKeyWindowCommand = ICommand<KeyDetailsDto?>.From(OpenSaveKeyWindow);
        DeleteKeyCommand = ICommand<KeyDetailsDto>.From(DeleteKey);
        RefreshCommand = ICommand.From(Refresh);
        Filter = string.Empty;
    }

    private void OpenSaveKeyWindow(KeyDetailsDto? key)
    {
        var vm = new SaveKeyWindowVM(_keysService, _organizationsService, key);
        var window = new SaveKeyWindow(vm);
        window.ShowDialog();
        RefreshCommand.Execute();
    }

    private async Task DeleteKey(KeyDetailsDto key)
    {
        try
        {
            await _keysService.Delete(key.Id);
            _keys.Remove(key);
            FilteredKeys.Refresh();
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
            _keys = [.. await _keysService.GetAll()];
            Organizations = [.. await _organizationsService.GetAll()];
            Organizations.Insert(0, new OrganizationDetailsDto(-1, string.Empty));
            FilteredKeys = CollectionViewSource.GetDefaultView(_keys);
            FilteredKeys.Filter = SetFilter;
            FilteredKeys.SortDescriptions.Add(new SortDescription(nameof(KeyDetailsDto.OrganizationId), ListSortDirection.Ascending));
            FilteredKeys.SortDescriptions.Add(new SortDescription(nameof(KeyDetailsDto.Id), ListSortDirection.Ascending));
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private bool SetFilter(object item)
    {
        var key = item as KeyDetailsDto;
        
        var isSelectedOrganization = _selectedOrganization is null ||
                                     _selectedOrganization.Id == -1 ||
                                     _selectedOrganization.Id == key?.OrganizationId;
        
        if (string.IsNullOrWhiteSpace(Filter) && isSelectedOrganization)
        {
            return true;
        }
        
        return (key?.OrganizationName.Contains(Filter, StringComparison.InvariantCultureIgnoreCase) == true || 
                key?.KeyValue.ToString().Contains(Filter, StringComparison.InvariantCultureIgnoreCase) == true) &&
               isSelectedOrganization;
    }

    private void ApplyFilter()
    {
        FilteredKeys?.Refresh();
    }
}
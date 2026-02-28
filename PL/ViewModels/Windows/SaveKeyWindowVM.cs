using System.Collections.ObjectModel;
using System.Windows;
using BLL.Services;
using Contracts.Dtos;
using Primitives.Enums;
using Utilities.Commands;

namespace PL.ViewModels.Windows;

public class SaveKeyWindowVM : BaseVM
{
    private readonly IKeysService  _keysService;
    private readonly IOrganizationsService _organizationsService;
    private readonly long? _id;
    
    private long? _organizationId;

    private Guid _keyValue;
    public Guid KeyValue
    {
        get => _keyValue;
        set => SetField(ref _keyValue, value);
    }

    private DateTime _startedAt;
    public DateTime StartedAt
    {
        get => _startedAt;
        set => SetField(ref _startedAt, value);
    }
    
    private DateTime _endedAt;
    public DateTime EndedAt
    {
        get => _endedAt; 
        set => SetField(ref _endedAt, value);
    }
    
    private ObservableCollection<OrganizationDetailsDto> _organizations;
    public ObservableCollection<OrganizationDetailsDto> Organizations
    {
        get => _organizations;
        set => SetField(ref _organizations, value);
    }
    
    private OrganizationDetailsDto _organization;
    public OrganizationDetailsDto Organization
    {
        get => _organization;
        set => SetField(ref _organization, value);
    }
    
    private KeyStatus _keyStatus;
    public KeyStatus KeyStatus
    {
        get => _keyStatus;
        set => SetField(ref _keyStatus, value);
    }

    public List<KeyStatus> KeyStatuses { get; set; }
    
    public ICommand SaveKeyCommand { get; }
    public ICommand CloseCreationCommand { get; set; }
    public ICommand GenerateKeyCommand { get; }
    public ICommand RefreshCommand { get; }

    public SaveKeyWindowVM(
        IKeysService keysService,
        IOrganizationsService organizationsService,
        KeyDetailsDto? keyDetailsDto = null)
    {
        _keysService = keysService;
        _organizationsService = organizationsService;
        
        SaveKeyCommand = ICommand.From(SaveKey);
        GenerateKeyCommand = ICommand.From(GenerateKey);
        RefreshCommand = ICommand.From(Refresh);

        KeyStatuses = Enum.GetValues<KeyStatus>().ToList();
        
        StartedAt = DateTime.Now;
        EndedAt = DateTime.Now;
        
        if (keyDetailsDto is not null)
        {
            _id = keyDetailsDto.Id;
            KeyValue = keyDetailsDto.KeyValue;
            StartedAt = keyDetailsDto.StartedAt;
            EndedAt = keyDetailsDto.EndedAt;
            _organizationId = keyDetailsDto.OrganizationId;
            KeyStatus = keyDetailsDto.KeyStatus;
        }
    }
    
    private async Task SaveKey()
    {
        try
        {
            if (_id is not null)
            {
                await _keysService.Update(new KeyDetailsDto(
                    _id.Value,
                    KeyValue,
                    StartedAt,
                    EndedAt,
                    Organization.Id,
                    Organization.Name,
                    KeyStatus));
            }
            else
            {
                await _keysService.Create(new KeyCreateDto(
                    KeyValue,
                    StartedAt,
                    EndedAt,
                    Organization.Id,
                    KeyStatus));
            }
            
            CloseCreationCommand.Execute();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    private void GenerateKey()
    {
        KeyValue = Guid.NewGuid();
    }

    protected override async Task Refresh()
    {
        try
        {
            Organizations = [.. await _organizationsService.GetAll()];

            if (_organizationId is not null)
            {
                Organization = Organizations.First(o => o.Id == _organizationId.Value);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
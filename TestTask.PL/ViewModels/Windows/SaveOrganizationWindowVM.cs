using System.Windows;
using TestTask.BLL.Services;
using TestTask.Contracts;
using TestTask.Contracts.Dtos;
using TestTask.Utilities.Commands;

namespace TestTask.PL.ViewModels.Windows;

public class SaveOrganizationWindowVM : BaseVM
{
    private readonly IOrganizationsService _organizationsService;
    private readonly long? _id;
    
    private string _name;
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    public ICommand SaveOrganizationCommand { get; }
    public ICommand CloseCreationCommand { get; set; }

    public SaveOrganizationWindowVM(
        IOrganizationsService organizationsService,
        OrganizationDetailsDto? organizationDetailsDto = null)
    {
        _organizationsService = organizationsService;
        SaveOrganizationCommand = ICommand.From(SaveOrganization);
        
        if (organizationDetailsDto is not null)
        {
            _id = organizationDetailsDto.Id;
            Name =  organizationDetailsDto.Name;
        }
    }

    private async Task SaveOrganization()
    {
        try
        {
            if (_id is not null)
            {
                await _organizationsService.Update(new OrganizationDetailsDto(_id.Value, Name));
            }
            else
            {
                await _organizationsService.Create(new OrganizationCreateDto(Name));
            }
            
            CloseCreationCommand.Execute();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
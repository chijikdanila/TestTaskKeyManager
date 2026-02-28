using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using PL.ViewModels.UserControls;
using PL.Views.UserControls;

namespace PL.ViewModels.Windows;

public class MainWindowVM : BaseVM
{
    public ObservableCollection<BaseTabVM> Tabs { get; set; }

    public MainWindowVM(IServiceProvider serviceProvider)
    {
        var organizationsTabVM = serviceProvider.GetRequiredService<OrganizationsTabVM>();
        organizationsTabVM.Title = "Организации";
        organizationsTabVM.Content = new OrganizationsTab();
        
        var keysTabVM = serviceProvider.GetRequiredService<KeysTabVM>();
        keysTabVM.Title = "Ключи";
        keysTabVM.Content = new KeysTab();

        Tabs = new ObservableCollection<BaseTabVM>
        {
            organizationsTabVM,
            keysTabVM
        };
    }
}
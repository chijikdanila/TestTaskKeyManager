using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using TestTask.PL.ViewModels.UserControls;
using TestTask.Utilities.DependencyServices;

namespace TestTask.PL.Views.UserControls;

public partial class OrganizationsTab : UserControl
{
    private readonly OrganizationsTabVM _vm;
    
    public OrganizationsTab()
    {
        _vm = DependencyResolver.ServiceProvider.GetRequiredService<OrganizationsTabVM>();
        DataContext = _vm;
        InitializeComponent();
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _vm.RefreshCommand.Execute();
    }
}
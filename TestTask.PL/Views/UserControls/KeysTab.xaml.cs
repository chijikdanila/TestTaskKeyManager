using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using TestTask.PL.ViewModels.UserControls;
using TestTask.Utilities.DependencyServices;

namespace TestTask.PL.Views.UserControls;

public partial class KeysTab : UserControl
{
    private readonly KeysTabVM _vm;
    
    public KeysTab()
    {
        _vm = DependencyResolver.ServiceProvider.GetRequiredService<KeysTabVM>();
        DataContext = _vm;
        InitializeComponent();
        
        Loaded += OnLoaded;
    }
    
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _vm.RefreshCommand.Execute();
    }
}
using System.Windows;
using TestTask.PL.ViewModels.Windows;
using TestTask.Utilities.Commands;

namespace TestTask.PL.Views.Windows;

public partial class SaveOrganizationWindow : Window
{
    public SaveOrganizationWindow(SaveOrganizationWindowVM vm)
    {
        vm.CloseCreationCommand = ICommand.From(this.Close);
        DataContext = vm;
        InitializeComponent();
    }
}
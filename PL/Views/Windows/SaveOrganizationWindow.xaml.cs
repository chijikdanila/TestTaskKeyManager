using System.Windows;
using PL.ViewModels.Windows;
using Utilities.Commands;

namespace PL.Views.Windows;

public partial class SaveOrganizationWindow : Window
{
    public SaveOrganizationWindow(SaveOrganizationWindowVM vm)
    {
        vm.CloseCreationCommand = ICommand.From(this.Close);
        DataContext = vm;
        InitializeComponent();
    }
}
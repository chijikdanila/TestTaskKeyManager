using System.Windows;
using TestTask.PL.ViewModels.Windows;
using TestTask.Utilities.Commands;

namespace TestTask.PL.Views.Windows;

public partial class SaveKeyWindow : Window
{
    private readonly SaveKeyWindowVM _vm;
    
    public SaveKeyWindow(SaveKeyWindowVM vm)
    {
        _vm = vm;
        vm.CloseCreationCommand = ICommand.From(this.Close);
        DataContext = vm;
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            _vm.RefreshCommand.Execute();
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }
    }
}
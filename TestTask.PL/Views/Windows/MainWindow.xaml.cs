using System.Windows;
using TestTask.PL.ViewModels.Windows;

namespace TestTask.PL.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowVM mainWindowVM)
    {
        DataContext = mainWindowVM;
        InitializeComponent();
    }
}
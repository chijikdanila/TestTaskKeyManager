using System.Windows;
using PL.ViewModels.Windows;

namespace PL.Views.Windows;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowVM mainWindowVM)
    {
        DataContext = mainWindowVM;
        InitializeComponent();
    }
}
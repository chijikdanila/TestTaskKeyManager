using System.Windows.Controls;

namespace TestTask.PL.ViewModels;

public class BaseTabVM : BaseVM
{
    private string _title;
    public string Title
    {
        get => _title;
        set => SetField(ref _title, value);
    }
    
    private UserControl _content;
    public UserControl Content
    {
        get => _content;
        set => SetField(ref _content, value);
    }
}
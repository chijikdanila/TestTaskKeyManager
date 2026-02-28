using System.Globalization;
using System.Windows.Data;
using TestTask.Primitives.Enums;

namespace TestTask.PL.Converters;

public class KeyStatusToMessageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is KeyStatus keyBlockStatus)
        {
            return keyBlockStatus.ToMessage();
        }
        
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
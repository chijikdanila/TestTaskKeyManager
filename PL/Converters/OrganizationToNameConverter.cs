using System.Globalization;
using System.Windows.Data;
using Contracts.Dtos;

namespace PL.Converters;

public class OrganizationToNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not OrganizationDetailsDto organization)
        {
            return string.Empty;
        }
        
        return organization.Name;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
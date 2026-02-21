using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NeuroLingo.WPF.Converters;

/// <summary>
/// Converts a boolean to a color (green for true, muted gray for false).
/// Used for password rule validation indicators.
/// </summary>
public class BoolToColorConverter : IValueConverter
{
    private static readonly SolidColorBrush SuccessBrush = new(Color.FromRgb(40, 167, 69));
    private static readonly SolidColorBrush MutedBrush = new(Color.FromRgb(108, 117, 125));

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue && boolValue)
            return SuccessBrush;
        return MutedBrush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

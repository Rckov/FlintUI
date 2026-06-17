using System.Globalization;
using System.Windows.Data;

namespace FlintUI.Themes.Converters;

/// <summary>
/// Inverts a <see cref="bool"/> value. The operation is symmetric, so two-way binding works.
/// </summary>
public class InverseBoolConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not true;
    }

    /// <inheritdoc/>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not true;
    }
}

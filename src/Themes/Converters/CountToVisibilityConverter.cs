using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FlintUI.Themes.Converters;

/// <summary>
/// Converts a collection to <see cref="Visibility"/>:
/// empty or <see langword="null"/> becomes <see cref="Visibility.Collapsed"/>,
/// non-empty becomes <see cref="Visibility.Visible"/>.
/// Pass <c>ConverterParameter="Invert"</c> to swap the logic.
/// </summary>
public sealed class CountToVisibilityConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isVisible = value is ICollection { Count: > 0 };

        if (IsInvert(parameter))
        {
            isVisible = !isVisible;
        }

        return isVisible
            ? Visibility.Visible
            : Visibility.Collapsed;
    }

    /// <inheritdoc />
    /// <exception cref="NotSupportedException">
    /// Always thrown because this converter supports one-way conversion only.
    /// </exception>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    private static bool IsInvert(object? parameter)
    {
        return string.Equals(parameter as string, "Invert", StringComparison.OrdinalIgnoreCase);
    }
}

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FlintUI.Themes.Converters;

/// <summary>
/// Converts a string to <see cref="Visibility"/>:
/// <see langword="null"/> or <see cref="string.Empty"/> becomes <see cref="Visibility.Collapsed"/>,
/// any other string becomes <see cref="Visibility.Visible"/>.
/// Pass <c>ConverterParameter="Invert"</c> to swap the logic.
/// </summary>
public sealed class EmptyStringToVisibilityConverter : IValueConverter
{
    private const string InvertParameter = "Invert";

    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var isVisible = !string.IsNullOrEmpty(value as string);

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
        return string.Equals(parameter as string, InvertParameter, StringComparison.OrdinalIgnoreCase);
    }
}

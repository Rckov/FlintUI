using System.Globalization;
using System.Windows.Data;

namespace FlintUI.Themes.Converters;

    /// <summary>
    /// Returns <see langword="true"/> when the bound enum value equals <c>ConverterParameter</c>.
    /// The parameter accepts either the enum value itself or its string name.
    /// <c>ConvertBack</c> writes the parameter back when the binding sends <see langword="true"/>,
    /// and <see cref="Binding.DoNothing"/> otherwise.
    /// </summary>
public sealed class EnumToBoolConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null || parameter is null)
        {
            return false;
        }

        return parameter switch
        {
            string name when value.GetType().IsEnum =>
                string.Equals(
                    Enum.GetName(value.GetType(), value),
                    name,
                    StringComparison.Ordinal),

            _ => Equals(value, parameter)
        };
    }

    /// <inheritdoc />
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true && parameter is not null
            ? parameter
            : Binding.DoNothing;
    }
}

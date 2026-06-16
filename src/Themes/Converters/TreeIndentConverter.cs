using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FlintUI.Themes.Converters;

/// <summary>
/// Converts a <see cref="System.Windows.Controls.TreeViewItem"/> into a left <see cref="System.Windows.Thickness"/>
/// that indents it according to its depth in the tree. Use for one-way binding in tree view templates.
/// </summary>
public class TreeIndentConverter : IValueConverter
{
    private double Indent => 16d;

    /// <inheritdoc/>
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var left = 0d;
        var element = value as DependencyObject;

        while (element is not null and not TreeView)
        {
            element = VisualTreeHelper.GetParent(element);
            if (element is TreeViewItem)
            {
                left += Indent;
            }
        }

        return new Thickness(left, 0, 0, 0);
    }

    /// <inheritdoc/>
    /// <exception cref="NotSupportedException">Always thrown; this converter is one-way only.</exception>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
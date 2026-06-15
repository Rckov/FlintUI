using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FlintUI.Themes.Converters;

public class TreeIndentConverter : IValueConverter
{
    private double Indent => 16d;

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

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
using System.Windows;
using System.Windows.Controls;

namespace FlintUI.Controls;

/// <summary>
/// An indeterminate progress indicator that shows a continuously animated spinner.
/// </summary>
public class Spinner : Control
{
    static Spinner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Spinner), new FrameworkPropertyMetadata(typeof(Spinner)));
    }
}
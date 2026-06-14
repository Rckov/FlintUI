using System.Windows;
using System.Windows.Controls;

namespace FlintUI.Controls;

public class Spinner : Control
{
    static Spinner()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Spinner), new FrameworkPropertyMetadata(typeof(Spinner)));
    }
}

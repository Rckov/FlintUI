using System.Windows;
using System.Windows.Controls.Primitives;

namespace FlintUI.Controls;

/// <summary>
/// A sliding on/off switch. Behaves like a <see cref="ToggleButton"/> with switch styling.
/// </summary>
public class ToggleSwitch : ToggleButton
{
    static ToggleSwitch()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleSwitch),
            new FrameworkPropertyMetadata(typeof(ToggleSwitch)));
    }
}
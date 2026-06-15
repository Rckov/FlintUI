using System.Windows;
using System.Windows.Controls;

namespace FlintUI.Controls;

public enum BadgeType
{
    Default,
    Accent,
    Success,
    Warning,
    Danger
}

public class Badge : ContentControl
{
    public static readonly DependencyProperty BadgeTypeProperty = DependencyProperty
        .Register(nameof(BadgeType), typeof(BadgeType), typeof(Badge), new PropertyMetadata(BadgeType.Default));

    static Badge()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));
    }

    public BadgeType BadgeType
    {
        get => (BadgeType)GetValue(BadgeTypeProperty);
        set => SetValue(BadgeTypeProperty, value);
    }
}
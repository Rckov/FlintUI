using System.Windows;
using System.Windows.Controls;

namespace FlintUI.Controls;

/// <summary>
/// The visual style of a <see cref="Badge"/>, which determines its color.
/// </summary>
public enum BadgeType
{
    /// <summary>
    /// Neutral styling with no semantic color.
    /// </summary>
    Default,

    /// <summary>
    /// The application accent color.
    /// </summary>
    Accent,

    /// <summary>
    /// Green styling for positive or completed states.
    /// </summary>
    Success,

    /// <summary>
    /// Amber styling for cautionary states.
    /// </summary>
    Warning,

    /// <summary>
    /// Red styling for errors or destructive states.
    /// </summary>
    Danger
}

/// <summary>
/// A small label that highlights a status, count, or category next to other content.
/// </summary>
public class Badge : ContentControl
{
    /// <summary>
    /// Dependency property for <see cref="BadgeType"/>.
    /// </summary>
    public static readonly DependencyProperty BadgeTypeProperty = DependencyProperty
        .Register(nameof(BadgeType), typeof(BadgeType), typeof(Badge), new PropertyMetadata(BadgeType.Default));

    static Badge()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));
    }

    /// <summary>
    /// The color style. Default is <see cref="BadgeType.Default"/>.
    /// </summary>
    public BadgeType BadgeType
    {
        get => (BadgeType)GetValue(BadgeTypeProperty);
        set => SetValue(BadgeTypeProperty, value);
    }
}
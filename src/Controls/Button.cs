using System.Windows;

namespace FlintUI.Controls;

/// <summary>
/// The visual style of a <see cref="Button"/>, which determines its emphasis and color.
/// </summary>
public enum ButtonKind
{
    /// <summary>
    /// Standard button styling.
    /// </summary>
    Default,

    /// <summary>
    /// Filled with the accent color to mark the primary action.
    /// </summary>
    Accent,

    /// <summary>
    /// Red styling for destructive actions.
    /// </summary>
    Danger,

    /// <summary>
    /// Transparent background with no border until hovered.
    /// </summary>
    Ghost
}

/// <summary>
/// A themed button that adds a style variant, an optional leading icon, and a configurable corner radius
/// on top of the standard WPF <see cref="System.Windows.Controls.Button"/>.
/// </summary>
public class Button : System.Windows.Controls.Button
{
    /// <summary>
    /// Dependency property for <see cref="ButtonKind"/>.
    /// </summary>
    public static readonly DependencyProperty ButtonKindProperty =
        DependencyProperty.Register(nameof(ButtonKind), typeof(ButtonKind), typeof(Button),
            new PropertyMetadata(ButtonKind.Default));

    /// <summary>
    /// Dependency property for <see cref="Icon"/>.
    /// </summary>
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(IconKind), typeof(Button),
            new PropertyMetadata(IconKind.None, OnIconChanged));

    private static readonly DependencyPropertyKey HasIconPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasIcon), typeof(bool), typeof(Button), new PropertyMetadata(false));

    /// <summary>
    /// Dependency property for <see cref="HasIcon"/>.
    /// </summary>
    public static readonly DependencyProperty HasIconProperty = HasIconPropertyKey.DependencyProperty;

    /// <summary>
    /// Dependency property for <see cref="IconSize"/>.
    /// </summary>
    public static readonly DependencyProperty IconSizeProperty =
        DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(Button), new PropertyMetadata(13d));

    /// <summary>
    /// Dependency property for <see cref="CornerRadius"/>.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Button),
            new PropertyMetadata(default(CornerRadius)));

    static Button()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(typeof(Button)));
    }

    /// <summary>
    /// The style variant that controls emphasis and color. Default is <see cref="ButtonKind.Default"/>.
    /// </summary>
    public ButtonKind ButtonKind
    {
        get => (ButtonKind)GetValue(ButtonKindProperty);
        set => SetValue(ButtonKindProperty, value);
    }

    /// <summary>
    /// The leading icon. <see cref="IconKind.None"/> hides the icon. Default is <see cref="IconKind.None"/>.
    /// </summary>
    public IconKind Icon
    {
        get => (IconKind)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    /// <summary>
    /// Whether an icon is assigned. Updated automatically when <see cref="Icon"/> changes.
    /// </summary>
    public bool HasIcon => (bool)GetValue(HasIconProperty);

    /// <summary>
    /// The icon size in device-independent units. Default is 13.
    /// </summary>
    public double IconSize
    {
        get => (double)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    /// <summary>
    /// The corner radius of the border. Default is <c>0,0,0,0</c>.
    /// </summary>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    private static void OnIconChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var button = (Button)d;
        var hasIcon = (IconKind)e.NewValue != IconKind.None;

        button.SetValue(HasIconPropertyKey, hasIcon);
    }
}
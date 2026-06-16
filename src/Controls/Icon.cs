using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlintUI.Controls;

/// <summary>
/// Displays a vector icon from the built-in <see cref="IconKind"/> set.
/// </summary>
public class Icon : Control
{
    /// <summary>
    /// Dependency property for <see cref="Kind"/>.
    /// </summary>
    public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(nameof(Kind), typeof(IconKind), typeof(Icon),
            new PropertyMetadata(IconKind.None, OnKindChanged));

    private static readonly DependencyPropertyKey DataPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(Data), typeof(Geometry), typeof(Icon), new PropertyMetadata());

    /// <summary>
    /// Dependency property for <see cref="Data"/>.
    /// </summary>
    public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

    /// <summary>
    /// Dependency property for <see cref="Size"/>.
    /// </summary>
    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(double), typeof(Icon), new FrameworkPropertyMetadata(16d));

    static Icon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
    }

    /// <summary>
    /// The icon to display. Setting this resolves and updates <see cref="Data"/>.
    /// Default is <see cref="IconKind.None"/>.
    /// </summary>
    public IconKind Kind
    {
        get => (IconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    /// <summary>
    /// The resolved geometry for the current <see cref="Kind"/>.
    /// Returns <see langword="null"/> when the kind is <see cref="IconKind.None"/>
    /// or the geometry resource was not found.
    /// </summary>
    public Geometry? Data
    {
        get => (Geometry?)GetValue(DataProperty);
        private set => SetValue(DataPropertyKey, value);
    }

    /// <summary>
    /// The icon size in device-independent units. Default is 16.
    /// </summary>
    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    private static void OnKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Icon icon)
        {
            return;
        }

        icon.Data = ResolveGeometry((IconKind)e.NewValue);
    }

    private static Geometry? ResolveGeometry(IconKind kind)
    {
        if (kind == IconKind.None)
        {
            return null;
        }

        var key = $"Icon.{kind}";
        var geometry = Application.Current?.TryFindResource(key) as Geometry;

#if DEBUG
        if (geometry is null && Application.Current is not null)
        {
            Debug.WriteLine($"Icon resource '{key}' not found.");
        }
#endif

        return geometry;
    }
}
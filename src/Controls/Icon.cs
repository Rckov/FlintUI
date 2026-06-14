using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlintUI.Controls;

public class Icon : Control
{
    public static readonly DependencyProperty KindProperty =
        DependencyProperty.Register(nameof(Kind), typeof(IconKind), typeof(Icon),
            new PropertyMetadata(IconKind.None, OnKindChanged));

    private static readonly DependencyPropertyKey DataPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(Data), typeof(Geometry), typeof(Icon), new PropertyMetadata());

    public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

    public static readonly DependencyProperty SizeProperty =
        DependencyProperty.Register(nameof(Size), typeof(double), typeof(Icon), new FrameworkPropertyMetadata(16d));

    static Icon()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
    }

    public IconKind Kind
    {
        get => (IconKind)GetValue(KindProperty);
        set => SetValue(KindProperty, value);
    }

    public Geometry? Data
    {
        get => (Geometry?)GetValue(DataProperty);
        private set => SetValue(DataPropertyKey, value);
    }

    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    private static void OnKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Icon icon) return;

        icon.Data = ResolveGeometry((IconKind)e.NewValue);
    }

    private static Geometry? ResolveGeometry(IconKind kind)
    {
        if (kind == IconKind.None) return null;

        var key = $"Icon.{kind}";
        var geometry = Application.Current?.TryFindResource(key) as Geometry;

#if DEBUG
        if (geometry is null && Application.Current is not null)
            Debug.WriteLine($"[FlintUI] Icon resource '{key}' not found.");
#endif

        return geometry;
    }
}
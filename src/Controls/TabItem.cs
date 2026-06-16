using System.Windows;

namespace FlintUI.Controls;

/// <summary>
/// A tab for use in <see cref="TabControl"/> that can opt out of showing a close button.
/// </summary>
public class TabItem : System.Windows.Controls.TabItem
{
    /// <summary>
    /// Dependency property for <see cref="CanClose"/>.
    /// </summary>
    public static readonly DependencyProperty CanCloseProperty =
        DependencyProperty.Register(nameof(CanClose), typeof(bool), typeof(TabItem), new PropertyMetadata(true));

    static TabItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem), new FrameworkPropertyMetadata(typeof(TabItem)));
    }

    /// <summary>
    /// Whether this tab shows a close button when the parent
    /// <see cref="TabControl.CloseButton"/> is enabled. Default is <see langword="true"/>.
    /// </summary>
    public bool CanClose
    {
        get => (bool)GetValue(CanCloseProperty);
        set => SetValue(CanCloseProperty, value);
    }
}
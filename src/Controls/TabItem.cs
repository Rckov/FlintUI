using System.Windows;

namespace FlintUI.Controls;

public class TabItem : System.Windows.Controls.TabItem
{
    static TabItem()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem), new FrameworkPropertyMetadata(typeof(TabItem)));
    }

    public static readonly DependencyProperty CanCloseProperty =
        DependencyProperty.Register(nameof(CanClose), typeof(bool), typeof(TabItem), new PropertyMetadata(true));

    public bool CanClose
    {
        get => (bool)GetValue(CanCloseProperty);
        set => SetValue(CanCloseProperty, value);
    }
}

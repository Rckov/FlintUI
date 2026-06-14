using System.Windows;

namespace FlintUI.Controls;

public delegate void TabClosingEventHandler(object sender, TabClosingEventArgs e);

public class TabClosingEventArgs : RoutedEventArgs
{
    public object? Item { get; }

    public bool Cancel { get; set; }

    internal TabClosingEventArgs(RoutedEvent routedEvent, object source, object? item)
        : base(routedEvent, source)
    {
        Item = item;
    }
}

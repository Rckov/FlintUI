using System.Windows;

namespace FlintUI.Controls;

/// <summary>
/// Represents the method that handles the <see cref="TabControl.TabClosing"/> event.
/// </summary>
/// <param name="sender">The <see cref="TabControl"/> raising the event.</param>
/// <param name="e">The event data, which carries the item being closed and lets a handler cancel the close.</param>
public delegate void TabClosingEventHandler(object sender, TabClosingEventArgs e);

/// <summary>
/// Provides data for the <see cref="TabControl.TabClosing"/> event and allows a handler to cancel the close.
/// </summary>
public class TabClosingEventArgs : RoutedEventArgs
{
    internal TabClosingEventArgs(RoutedEvent routedEvent, object source, object? item) : base(routedEvent, source)
    {
        Item = item;
    }

    /// <summary>
    /// The item about to be closed. This is the bound data item when tabs come from an items source,
    /// otherwise the <see cref="TabItem"/> itself.
    /// </summary>
    public object? Item { get; }

    /// <summary>
    /// Whether to cancel the close. Set to <see langword="true"/> in a handler
    /// to keep the tab open.
    /// </summary>
    public bool Cancel { get; set; }
}
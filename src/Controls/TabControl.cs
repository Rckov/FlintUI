using System.Windows;
using System.Windows.Input;

namespace FlintUI.Controls;

/// <summary>
/// A themed tab control with optional per-tab close buttons. Closing a tab raises <see cref="TabClosing"/>
/// (which can be cancelled) and then either invokes <see cref="CloseTabCommand"/> or removes the item directly.
/// </summary>
public class TabControl : System.Windows.Controls.TabControl
{
    /// <summary>
    /// The command bound to each tab's close button. Its parameter is the <see cref="TabItem"/> being closed.
    /// </summary>
    public static readonly RoutedUICommand CloseTab =
        new("Close Tab", nameof(CloseTab), typeof(TabControl));

    /// <summary>
    /// Routed event for <see cref="TabClosing"/>.
    /// </summary>
    public static readonly RoutedEvent TabClosingEvent =
        EventManager.RegisterRoutedEvent(nameof(TabClosing), RoutingStrategy.Direct, typeof(TabClosingEventHandler),
            typeof(TabControl));

    /// <summary>
    /// Dependency property for <see cref="CloseButton"/>.
    /// </summary>
    public static readonly DependencyProperty CloseButtonProperty =
        DependencyProperty.Register(nameof(CloseButton), typeof(bool), typeof(TabControl), new PropertyMetadata(false));

    /// <summary>
    /// Dependency property for <see cref="CloseTabCommand"/>.
    /// </summary>
    public static readonly DependencyProperty CloseTabCommandProperty =
        DependencyProperty.Register(nameof(CloseTabCommand), typeof(ICommand), typeof(TabControl),
            new PropertyMetadata(null));

    static TabControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControl), new FrameworkPropertyMetadata(typeof(TabControl)));
    }

    /// <summary>
    /// Initializes a new instance and wires up the close-tab command.
    /// </summary>
    public TabControl()
    {
        CommandBindings.Add(new CommandBinding(CloseTab, OnCloseTabExecuted));
    }

    /// <summary>
    /// Whether a close button is shown on each tab. Default is <see langword="false"/>.
    /// </summary>
    public bool CloseButton
    {
        get => (bool)GetValue(CloseButtonProperty);
        set => SetValue(CloseButtonProperty, value);
    }

    /// <summary>
    /// A command run when a tab is closed. When set, the control invokes this command instead of
    /// removing the item itself, letting a view model own the removal. The command parameter is the closed item.
    /// </summary>
    public ICommand? CloseTabCommand
    {
        get => (ICommand?)GetValue(CloseTabCommandProperty);
        set => SetValue(CloseTabCommandProperty, value);
    }

    /// <summary>
    /// Occurs when a tab is about to close. Handlers can set <see cref="TabClosingEventArgs.Cancel"/> to keep
    /// the tab open. Raised before <see cref="CloseTabCommand"/> runs or the item is removed.
    /// </summary>
    public event TabClosingEventHandler TabClosing
    {
        add => AddHandler(TabClosingEvent, value);
        remove => RemoveHandler(TabClosingEvent, value);
    }

    private void OnCloseTabExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        if (e.Parameter is not TabItem tab)
        {
            return;
        }

        var item = ItemContainerGenerator.ItemFromContainer(tab);
        if (item == DependencyProperty.UnsetValue)
        {
            item = tab;
        }

        if (CloseTabCommand is { } command && !command.CanExecute(item))
        {
            return;
        }

        var args = new TabClosingEventArgs(TabClosingEvent, this, item);
        RaiseEvent(args);
        if (args.Cancel)
        {
            return;
        }

        if (CloseTabCommand is not null)
        {
            CloseTabCommand.Execute(item);
        }
        else if (ItemsSource is null && Items.Contains(item))
        {
            Items.Remove(item);
        }
    }
}
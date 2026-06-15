using System.Windows;
using System.Windows.Input;

namespace FlintUI.Controls;

public class TabControl : System.Windows.Controls.TabControl
{
    public static readonly RoutedUICommand CloseTab =
        new("Close Tab", nameof(CloseTab), typeof(TabControl));

    public static readonly RoutedEvent TabClosingEvent =
        EventManager.RegisterRoutedEvent(nameof(TabClosing), RoutingStrategy.Direct, typeof(TabClosingEventHandler),
            typeof(TabControl));

    public static readonly DependencyProperty CloseButtonProperty =
        DependencyProperty.Register(nameof(CloseButton), typeof(bool), typeof(TabControl), new PropertyMetadata(false));

    public static readonly DependencyProperty CloseTabCommandProperty =
        DependencyProperty.Register(nameof(CloseTabCommand), typeof(ICommand), typeof(TabControl),
            new PropertyMetadata(null));

    static TabControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControl), new FrameworkPropertyMetadata(typeof(TabControl)));
    }

    public TabControl()
    {
        CommandBindings.Add(new CommandBinding(CloseTab, OnCloseTabExecuted));
    }

    public bool CloseButton
    {
        get => (bool)GetValue(CloseButtonProperty);
        set => SetValue(CloseButtonProperty, value);
    }

    public ICommand? CloseTabCommand
    {
        get => (ICommand?)GetValue(CloseTabCommandProperty);
        set => SetValue(CloseTabCommandProperty, value);
    }

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
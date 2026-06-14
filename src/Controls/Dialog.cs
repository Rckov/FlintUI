using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace FlintUI.Controls;

public enum DialogButton
{
    Ok,
    OkCancel,
    YesNo,
    YesNoCancel
}

public enum DialogResult
{
    None,
    Ok,
    Cancel,
    Yes,
    No
}

public enum DialogIcon
{
    None,
    Info,
    Success,
    Warning,
    Error,
    Question
}

[TemplatePart(Name = "PART_Ok", Type = typeof(ButtonBase))]
[TemplatePart(Name = "PART_Cancel", Type = typeof(ButtonBase))]
[TemplatePart(Name = "PART_Yes", Type = typeof(ButtonBase))]
[TemplatePart(Name = "PART_No", Type = typeof(ButtonBase))]
public class DialogWindow : Window
{
    public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register(nameof(Caption), typeof(string), typeof(DialogWindow),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string), typeof(DialogWindow),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty ButtonsProperty =
        DependencyProperty.Register(nameof(Buttons), typeof(DialogButton), typeof(DialogWindow),
            new PropertyMetadata(DialogButton.Ok));

    public static readonly DependencyProperty SymbolProperty =
        DependencyProperty.Register(nameof(Symbol), typeof(DialogIcon), typeof(DialogWindow),
            new PropertyMetadata(DialogIcon.None));

    static DialogWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogWindow),
            new FrameworkPropertyMetadata(typeof(DialogWindow)));
    }

    public DialogWindow()
    {
        WindowStyle = WindowStyle.None;
        AllowsTransparency = true;
        ResizeMode = ResizeMode.NoResize;
        ShowInTaskbar = false;
        SizeToContent = SizeToContent.WidthAndHeight;
        Background = Brushes.Transparent;
        SetResourceReference(StyleProperty, typeof(DialogWindow));
    }

    public string Caption
    {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }

    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public DialogButton Buttons
    {
        get => (DialogButton)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }

    public DialogIcon Symbol
    {
        get => (DialogIcon)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    public DialogResult Result { get; private set; }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        Wire("PART_Ok", Controls.DialogResult.Ok);
        Wire("PART_Cancel", Controls.DialogResult.Cancel);
        Wire("PART_Yes", Controls.DialogResult.Yes);
        Wire("PART_No", Controls.DialogResult.No);
    }

    private void Wire(string part, DialogResult result)
    {
        if (GetTemplateChild(part) is ButtonBase button)
            button.Click += (_, _) =>
            {
                Result = result;
                Close();
            };
    }
}

public static class Dialog
{
    public static DialogResult Show(string message, string caption = "", DialogButton buttons = DialogButton.Ok,
        DialogIcon icon = DialogIcon.None, Window? owner = null)
    {
        var dialog = new DialogWindow
        {
            Message = message,
            Caption = caption,
            Buttons = buttons,
            Symbol = icon,
            Owner = owner ?? Application.Current?.Windows.OfType<Window>().FirstOrDefault(window => window.IsActive)
        };

        dialog.WindowStartupLocation =
            dialog.Owner is null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;
        dialog.ShowDialog();

        return dialog.Result;
    }
}
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace FlintUI.Controls;

/// <summary>
/// The set of buttons shown on a dialog.
/// </summary>
public enum DialogButton
{
    /// <summary>
    /// A single OK button.
    /// </summary>
    Ok,

    /// <summary>
    /// OK and Cancel buttons.
    /// </summary>
    OkCancel,

    /// <summary>
    /// Yes and No buttons.
    /// </summary>
    YesNo,

    /// <summary>
    /// Yes, No, and Cancel buttons.
    /// </summary>
    YesNoCancel
}

/// <summary>
/// Identifies the button a user clicked to close a dialog.
/// </summary>
public enum DialogResult
{
    /// <summary>
    /// The dialog was closed without a button being clicked.
    /// </summary>
    None,

    /// <summary>
    /// The OK button was clicked.
    /// </summary>
    Ok,

    /// <summary>
    /// The Cancel button was clicked.
    /// </summary>
    Cancel,

    /// <summary>
    /// The Yes button was clicked.
    /// </summary>
    Yes,

    /// <summary>
    /// The No button was clicked.
    /// </summary>
    No
}

/// <summary>
/// The icon shown on a dialog to convey the nature of its message.
/// </summary>
public enum DialogIcon
{
    /// <summary>
    /// No icon.
    /// </summary>
    None,

    /// <summary>
    /// An informational icon.
    /// </summary>
    Info,

    /// <summary>
    /// A success icon.
    /// </summary>
    Success,

    /// <summary>
    /// A warning icon.
    /// </summary>
    Warning,

    /// <summary>
    /// An error icon.
    /// </summary>
    Error,

    /// <summary>
    /// A question icon, typically used for confirmations.
    /// </summary>
    Question
}

/// <summary>
/// The window that hosts a themed dialog. Prefer the static <see cref="Dialog"/> helper over
/// creating instances directly.
/// </summary>
[TemplatePart(Name = "PART_Ok", Type = typeof(ButtonBase))]
[TemplatePart(Name = "PART_Cancel", Type = typeof(ButtonBase))]
[TemplatePart(Name = "PART_Yes", Type = typeof(ButtonBase))]
[TemplatePart(Name = "PART_No", Type = typeof(ButtonBase))]
public class DialogWindow : Window
{
    /// <summary>
    /// Dependency property for <see cref="Caption"/>.
    /// </summary>
    public static readonly DependencyProperty CaptionProperty =
        DependencyProperty.Register(nameof(Caption), typeof(string), typeof(DialogWindow),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Dependency property for <see cref="Message"/>.
    /// </summary>
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(string), typeof(DialogWindow),
            new PropertyMetadata(string.Empty));

    /// <summary>
    /// Dependency property for <see cref="Buttons"/>.
    /// </summary>
    public static readonly DependencyProperty ButtonsProperty =
        DependencyProperty.Register(nameof(Buttons), typeof(DialogButton), typeof(DialogWindow),
            new PropertyMetadata(DialogButton.Ok));

    /// <summary>
    /// Dependency property for <see cref="Symbol"/>.
    /// </summary>
    public static readonly DependencyProperty SymbolProperty =
        DependencyProperty.Register(nameof(Symbol), typeof(DialogIcon), typeof(DialogWindow),
            new PropertyMetadata(DialogIcon.None));

    static DialogWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DialogWindow),
            new FrameworkPropertyMetadata(typeof(DialogWindow)));
    }

    /// <summary>
    /// Initializes a borderless, transparent dialog window sized to its content.
    /// </summary>
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

    /// <summary>
    /// The dialog title. When <see langword="null"/>, the title bar is hidden.
    /// </summary>
    public string? Caption
    {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }

    /// <summary>
    /// The body text shown to the user.
    /// </summary>
    public string Message
    {
        get => (string)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    /// <summary>
    /// The set of buttons shown on the dialog. Default is <see cref="DialogButton.Ok"/>.
    /// </summary>
    public DialogButton Buttons
    {
        get => (DialogButton)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }

    /// <summary>
    /// The icon that conveys the nature of the message. Default is <see cref="DialogIcon.None"/>.
    /// </summary>
    public DialogIcon Symbol
    {
        get => (DialogIcon)GetValue(SymbolProperty);
        set => SetValue(SymbolProperty, value);
    }

    /// <summary>
    /// The button the user clicked. Returns <see cref="DialogResult.None"/> until the
    /// dialog is closed by a button click.
    /// </summary>
    public DialogResult Result { get; private set; }

    /// <inheritdoc/>
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
        {
            button.Click += (_, _) =>
            {
                Result = result;
                Close();
            };
        }
    }
}

/// <summary>
/// Static entry point for themed modal dialogs. Use this instead of creating
/// <see cref="DialogWindow"/> instances directly.
/// </summary>
public static class Dialog
{
    /// <summary>
    /// Shows a modal dialog and waits for the user to close it.
    /// </summary>
    /// <param name="message">The body text shown to the user.</param>
    /// <param name="caption">The dialog title. When <see langword="null"/>, the title bar is hidden.</param>
    /// <param name="buttons">The set of buttons to display. Default is <see cref="DialogButton.Ok"/>.</param>
    /// <param name="icon">The icon that conveys the nature of the message. Default is <see cref="DialogIcon.None"/>.</param>
    /// <param name="owner">
    /// The owner window. When <see langword="null"/>, the active application window is used if one exists;
    /// otherwise the dialog is centered on the screen.
    /// </param>
    /// <returns>The button the user clicked, or <see cref="DialogResult.None"/> if the dialog was dismissed.</returns>
    public static DialogResult Show(string message, string? caption = null, DialogButton buttons = DialogButton.Ok,
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

        dialog.WindowStartupLocation = dialog.Owner is null
            ? WindowStartupLocation.CenterScreen
            : WindowStartupLocation.CenterOwner;
        dialog.ShowDialog();

        return dialog.Result;
    }
}
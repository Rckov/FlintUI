using System.Windows;
using System.Windows.Controls;

namespace FlintUI.Controls;

/// <summary>
/// A themed password entry control that adds two-way <see cref="Password"/> binding, a placeholder, and an
/// optional reveal button on top of the standard WPF password box.
/// </summary>
/// <remarks>
/// The <see cref="Password"/> value is stored and exposed as plain text to support binding. Unlike the
/// standard <see cref="System.Windows.Controls.PasswordBox"/>, it is not kept in protected memory, so it is
/// only as secure as the surrounding application.
/// </remarks>
[TemplatePart(Name = "PART_PasswordBox", Type = typeof(System.Windows.Controls.PasswordBox))]
public class PasswordBox : Control
{
    /// <summary>
    /// Dependency property for <see cref="Password"/>.
    /// </summary>
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (d, e) => ((PasswordBox)d).OnPasswordChanged((string)(e.NewValue ?? string.Empty))));

    /// <summary>
    /// Dependency property for <see cref="ShowPassword"/>.
    /// </summary>
    public static readonly DependencyProperty ShowPasswordProperty =
        DependencyProperty.Register(nameof(ShowPassword), typeof(bool), typeof(PasswordBox),
            new PropertyMetadata(false));

    /// <summary>
    /// Dependency property for <see cref="Placeholder"/>.
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PasswordBox),
            new PropertyMetadata(null));

    /// <summary>
    /// Dependency property for <see cref="RevealButton"/>.
    /// </summary>
    public static readonly DependencyProperty RevealButtonProperty =
        DependencyProperty.Register(nameof(RevealButton), typeof(bool), typeof(PasswordBox),
            new PropertyMetadata(true));

    private static readonly DependencyPropertyKey HasTextPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool), typeof(PasswordBox),
            new PropertyMetadata(false));

    /// <summary>
    /// Dependency property for <see cref="HasText"/>.
    /// </summary>
    public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;
    private System.Windows.Controls.PasswordBox? _passwordBox;

    static PasswordBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox),
            new FrameworkPropertyMetadata(typeof(PasswordBox)));
    }

    /// <summary>
    /// The entered password as plain text. Binds two-way by default.
    /// </summary>
    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    /// <summary>
    /// Whether the password is shown as plain text instead of masked.
    /// Default is <see langword="false"/>.
    /// </summary>
    public bool ShowPassword
    {
        get => (bool)GetValue(ShowPasswordProperty);
        set => SetValue(ShowPasswordProperty, value);
    }

    /// <summary>
    /// The hint text shown while the box is empty. Default is <see langword="null"/>.
    /// </summary>
    public string? Placeholder
    {
        get => (string?)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    /// <summary>
    /// Whether the reveal button that toggles <see cref="ShowPassword"/> is shown.
    /// Default is <see langword="true"/>.
    /// </summary>
    public bool RevealButton
    {
        get => (bool)GetValue(RevealButtonProperty);
        set => SetValue(RevealButtonProperty, value);
    }

    /// <summary>
    /// Whether the box contains any characters. Updated automatically when the password changes.
    /// </summary>
    public bool HasText => (bool)GetValue(HasTextProperty);

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_passwordBox is not null)
        {
            _passwordBox.PasswordChanged -= OnPasswordBoxChanged;
        }

        _passwordBox = GetTemplateChild("PART_PasswordBox") as System.Windows.Controls.PasswordBox;

        if (_passwordBox is not null)
        {
            _passwordBox.PasswordChanged += OnPasswordBoxChanged;
        }

        OnPasswordChanged(Password);
    }

    private void OnPasswordBoxChanged(object sender, RoutedEventArgs e)
    {
        SetValue(PasswordProperty, _passwordBox!.Password);
    }

    private void OnPasswordChanged(string value)
    {
        if (_passwordBox is not null && _passwordBox.Password != value)
        {
            _passwordBox.Password = value;
        }

        SetValue(HasTextPropertyKey, value.Length > 0);
    }
}
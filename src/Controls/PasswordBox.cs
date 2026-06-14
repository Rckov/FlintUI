using System.Windows;
using System.Windows.Controls;

namespace FlintUI.Controls;

[TemplatePart(Name = "PART_PasswordBox", Type = typeof(System.Windows.Controls.PasswordBox))]
public class PasswordBox : Control
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register(nameof(Password), typeof(string), typeof(PasswordBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (d, e) => ((PasswordBox)d).OnPasswordChanged((string)(e.NewValue ?? string.Empty))));

    public static readonly DependencyProperty ShowPasswordProperty =
        DependencyProperty.Register(nameof(ShowPassword), typeof(bool), typeof(PasswordBox),
            new PropertyMetadata(false));

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(PasswordBox),
            new PropertyMetadata(null));

    public static readonly DependencyProperty RevealButtonProperty =
        DependencyProperty.Register(nameof(RevealButton), typeof(bool), typeof(PasswordBox),
            new PropertyMetadata(true));

    private static readonly DependencyPropertyKey HasTextPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool), typeof(PasswordBox),
            new PropertyMetadata(false));

    public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;
    private System.Windows.Controls.PasswordBox? _passwordBox;

    static PasswordBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox),
            new FrameworkPropertyMetadata(typeof(PasswordBox)));
    }

    public string Password
    {
        get => (string)GetValue(PasswordProperty);
        set => SetValue(PasswordProperty, value);
    }

    public bool ShowPassword
    {
        get => (bool)GetValue(ShowPasswordProperty);
        set => SetValue(ShowPasswordProperty, value);
    }

    public string? Placeholder
    {
        get => (string?)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public bool RevealButton
    {
        get => (bool)GetValue(RevealButtonProperty);
        set => SetValue(RevealButtonProperty, value);
    }

    public bool HasText => (bool)GetValue(HasTextProperty);

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_passwordBox is not null) _passwordBox.PasswordChanged -= OnPasswordBoxChanged;

        _passwordBox = GetTemplateChild("PART_PasswordBox") as System.Windows.Controls.PasswordBox;

        if (_passwordBox is not null) _passwordBox.PasswordChanged += OnPasswordBoxChanged;

        OnPasswordChanged(Password);
    }

    private void OnPasswordBoxChanged(object sender, RoutedEventArgs e)
    {
        SetValue(PasswordProperty, _passwordBox!.Password);
    }

    private void OnPasswordChanged(string value)
    {
        if (_passwordBox is not null && _passwordBox.Password != value) _passwordBox.Password = value;

        SetValue(HasTextPropertyKey, value.Length > 0);
    }
}
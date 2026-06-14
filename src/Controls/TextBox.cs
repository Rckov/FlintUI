using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FlintUI.Controls;

[TemplatePart(Name = "PART_ClearButton", Type = typeof(ButtonBase))]
public class TextBox : System.Windows.Controls.TextBox
{
    public static readonly DependencyProperty ClearButtonProperty =
        DependencyProperty.Register(nameof(ClearButton), typeof(bool), typeof(TextBox), new PropertyMetadata(false));

    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBox), new PropertyMetadata(null));

    public static readonly DependencyProperty IconKindProperty =
        DependencyProperty.Register(nameof(IconKind), typeof(IconKind), typeof(TextBox),
            new PropertyMetadata(IconKind.None, OnIconKindChanged));

    private static readonly DependencyPropertyKey HasIconPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasIcon), typeof(bool), typeof(TextBox),
            new PropertyMetadata(false));

    private static readonly DependencyPropertyKey HasTextPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool), typeof(TextBox),
            new PropertyMetadata(false));

    public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

    public static readonly DependencyProperty HasIconProperty = HasIconPropertyKey.DependencyProperty;
    private ButtonBase? _clearButton;

    static TextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
    }

    public bool ClearButton
    {
        get => (bool)GetValue(ClearButtonProperty);
        set => SetValue(ClearButtonProperty, value);
    }

    public string? Placeholder
    {
        get => (string?)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public IconKind IconKind
    {
        get => (IconKind)GetValue(IconKindProperty);
        set => SetValue(IconKindProperty, value);
    }

    public bool HasIcon => (bool)GetValue(HasIconProperty);

    public bool HasText => (bool)GetValue(HasTextProperty);

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_clearButton is not null) _clearButton.Click -= OnClearClick;

        _clearButton = GetTemplateChild("PART_ClearButton") as ButtonBase;

        if (_clearButton is not null) _clearButton.Click += OnClearClick;
    }

    private void OnClearClick(object sender, RoutedEventArgs e)
    {
        Clear();
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);
        SetValue(HasTextPropertyKey, !string.IsNullOrEmpty(Text));
    }

    private static void OnIconKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var textBox = (TextBox)d;
        textBox.SetValue(HasIconPropertyKey, (IconKind)e.NewValue != IconKind.None);
    }
}
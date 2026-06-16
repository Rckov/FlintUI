using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FlintUI.Controls;

/// <summary>
/// A themed text box that adds a placeholder, an optional leading icon, and an optional clear button on top
/// of the standard WPF <see cref="System.Windows.Controls.TextBox"/>.
/// </summary>
[TemplatePart(Name = "PART_ClearButton", Type = typeof(ButtonBase))]
public class TextBox : System.Windows.Controls.TextBox
{
    /// <summary>
    /// Dependency property for <see cref="ClearButton"/>.
    /// </summary>
    public static readonly DependencyProperty ClearButtonProperty =
        DependencyProperty.Register(nameof(ClearButton), typeof(bool), typeof(TextBox), new PropertyMetadata(false));

    /// <summary>
    /// Dependency property for <see cref="Placeholder"/>.
    /// </summary>
    public static readonly DependencyProperty PlaceholderProperty =
        DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBox), new PropertyMetadata(null));

    /// <summary>
    /// Dependency property for <see cref="IconKind"/>.
    /// </summary>
    public static readonly DependencyProperty IconKindProperty =
        DependencyProperty.Register(nameof(IconKind), typeof(IconKind), typeof(TextBox),
            new PropertyMetadata(IconKind.None, OnIconKindChanged));

    private static readonly DependencyPropertyKey HasIconPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasIcon), typeof(bool), typeof(TextBox),
            new PropertyMetadata(false));

    private static readonly DependencyPropertyKey HasTextPropertyKey =
        DependencyProperty.RegisterReadOnly(nameof(HasText), typeof(bool), typeof(TextBox),
            new PropertyMetadata(false));

    /// <summary>
    /// Dependency property for <see cref="HasText"/>.
    /// </summary>
    public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

    /// <summary>
    /// Dependency property for <see cref="HasIcon"/>.
    /// </summary>
    public static readonly DependencyProperty HasIconProperty = HasIconPropertyKey.DependencyProperty;
    private ButtonBase? _clearButton;

    static TextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
    }

    /// <summary>
    /// Whether a clear button is shown that empties the text when clicked.
    /// Default is <see langword="false"/>.
    /// </summary>
    public bool ClearButton
    {
        get => (bool)GetValue(ClearButtonProperty);
        set => SetValue(ClearButtonProperty, value);
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
    /// The leading icon shown inside the box.
    /// <see cref="IconKind.None"/> hides the icon. Default is <see cref="IconKind.None"/>.
    /// </summary>
    public IconKind IconKind
    {
        get => (IconKind)GetValue(IconKindProperty);
        set => SetValue(IconKindProperty, value);
    }

    /// <summary>
    /// Whether a leading icon is assigned. Updated automatically when <see cref="IconKind"/> changes.
    /// </summary>
    public bool HasIcon => (bool)GetValue(HasIconProperty);

    /// <summary>
    /// Whether the box contains any text. Updated automatically when the text changes.
    /// </summary>
    public bool HasText => (bool)GetValue(HasTextProperty);

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_clearButton is not null)
        {
            _clearButton.Click -= OnClearClick;
        }

        _clearButton = GetTemplateChild("PART_ClearButton") as ButtonBase;

        if (_clearButton is not null)
        {
            _clearButton.Click += OnClearClick;
        }
    }

    private void OnClearClick(object sender, RoutedEventArgs e)
    {
        Clear();
    }

    /// <inheritdoc/>
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
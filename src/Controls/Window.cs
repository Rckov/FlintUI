using System.Windows;
using System.Windows.Shell;

namespace FlintUI.Controls;

/// <summary>
/// A themed window with a custom title bar that supports a subtitle and custom content on either side of the
/// caption. Use as the base class for application windows.
/// </summary>
/// <remarks>
/// The constructor is protected, so derive from it instead of instantiating it directly.
/// The window applies custom window chrome once its source is initialized.
/// </remarks>
[TemplatePart(Name = "PART_MinimizeButton", Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = "PART_MaximizeButton", Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = "PART_CloseButton", Type = typeof(System.Windows.Controls.Button))]
public class Window : System.Windows.Window
{
    /// <summary>
    /// Dependency property for <see cref="Subtitle"/>.
    /// </summary>
    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(Window),
            new PropertyMetadata(null, OnSubtitleChanged));

    /// <summary>
    /// Dependency property for <see cref="LeftContent"/>.
    /// </summary>
    public static readonly DependencyProperty LeftContentProperty =
        DependencyProperty.Register(nameof(LeftContent), typeof(object), typeof(Window), new PropertyMetadata(null));

    /// <summary>
    /// Dependency property for <see cref="RightContent"/>.
    /// </summary>
    public static readonly DependencyProperty RightContentProperty =
        DependencyProperty.Register(nameof(RightContent), typeof(object), typeof(Window), new PropertyMetadata(null));

    private static readonly DependencyProperty HasSubtitleProperty =
        DependencyProperty.Register(nameof(HasSubtitle), typeof(bool), typeof(Window), new PropertyMetadata(false));

    private System.Windows.Controls.Button? _closeButton;
    private System.Windows.Controls.Button? _maximizeButton;
    private System.Windows.Controls.Button? _minimizeButton;

    static Window()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
    }

    /// <summary>
    /// Initializes a new instance and applies the FlintUI window style.
    /// </summary>
    protected Window()
    {
        SetResourceReference(StyleProperty, typeof(Window));
    }

    /// <summary>
    /// The subtitle shown in the title bar next to the title. Default is <see langword="null"/>.
    /// </summary>
    public string? Subtitle
    {
        get => (string?)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    /// <summary>
    /// Custom content shown on the left side of the title bar, after the title.
    /// Default is <see langword="null"/>.
    /// </summary>
    public object? LeftContent
    {
        get => GetValue(LeftContentProperty);
        set => SetValue(LeftContentProperty, value);
    }

    /// <summary>
    /// Custom content shown on the right side of the title bar, before the caption buttons.
    /// Default is <see langword="null"/>.
    /// </summary>
    public object? RightContent
    {
        get => GetValue(RightContentProperty);
        set => SetValue(RightContentProperty, value);
    }

    /// <summary>
    /// Whether a subtitle is present. Updated automatically when <see cref="Subtitle"/> changes.
    /// </summary>
    public bool HasSubtitle
    {
        get => (bool)GetValue(HasSubtitleProperty);
        set => SetValue(HasSubtitleProperty, value);
    }

    /// <inheritdoc/>
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        WindowChrome.SetWindowChrome(this, new WindowChrome
        {
            CaptionHeight = 40,
            ResizeBorderThickness = new Thickness(4),
            GlassFrameThickness = new Thickness(1),
            CornerRadius = new CornerRadius(0),
            UseAeroCaptionButtons = false
        });
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_closeButton is not null)
        {
            _closeButton.Click -= OnCloseClick;
        }

        if (_minimizeButton is not null)
        {
            _minimizeButton.Click -= OnMinimizeClick;
        }

        if (_maximizeButton is not null)
        {
            _maximizeButton.Click -= OnMaximizeClick;
        }

        _closeButton = GetTemplateChild("PART_CloseButton") as System.Windows.Controls.Button;
        _minimizeButton = GetTemplateChild("PART_MinimizeButton") as System.Windows.Controls.Button;
        _maximizeButton = GetTemplateChild("PART_MaximizeButton") as System.Windows.Controls.Button;

        if (_closeButton is not null)
        {
            _closeButton.Click += OnCloseClick;
        }

        if (_minimizeButton is not null)
        {
            _minimizeButton.Click += OnMinimizeClick;
        }

        if (_maximizeButton is not null)
        {
            _maximizeButton.Click += OnMaximizeClick;
        }
    }

    private void OnCloseClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnMinimizeClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void OnMaximizeClick(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private static void OnSubtitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Window window)
        {
            window.HasSubtitle = !string.IsNullOrWhiteSpace(e.NewValue as string);
        }
    }
}
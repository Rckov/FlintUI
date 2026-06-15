using System.Windows;
using System.Windows.Shell;

namespace FlintUI.Controls;

[TemplatePart(Name = "PART_MinimizeButton", Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = "PART_MaximizeButton", Type = typeof(System.Windows.Controls.Button))]
[TemplatePart(Name = "PART_CloseButton", Type = typeof(System.Windows.Controls.Button))]
public class Window : System.Windows.Window
{
    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(Window),
            new PropertyMetadata(null, OnSubtitleChanged));

    public static readonly DependencyProperty LeftContentProperty =
        DependencyProperty.Register(nameof(LeftContent), typeof(object), typeof(Window), new PropertyMetadata(null));

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

    protected Window()
    {
        SetResourceReference(StyleProperty, typeof(Window));
    }

    public string? Subtitle
    {
        get => (string?)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public object? LeftContent
    {
        get => GetValue(LeftContentProperty);
        set => SetValue(LeftContentProperty, value);
    }

    public object? RightContent
    {
        get => GetValue(RightContentProperty);
        set => SetValue(RightContentProperty, value);
    }

    public bool HasSubtitle
    {
        get => (bool)GetValue(HasSubtitleProperty);
        set => SetValue(HasSubtitleProperty, value);
    }

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
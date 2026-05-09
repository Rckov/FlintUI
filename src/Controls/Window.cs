using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

namespace FlintUI.Controls;

[TemplatePart(Name = "PART_MinimizeButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_MaximizeButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
public class Window : System.Windows.Window
{
	static Window()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
	}

	public Window()
	{
		SetResourceReference(StyleProperty, typeof(Window));
		AddHandler(Button.ClickEvent, new RoutedEventHandler(OnWindowButtonClick));
	}

	public static readonly DependencyProperty SubtitleProperty =
		DependencyProperty.Register(nameof(Subtitle), typeof(string), typeof(Window), new PropertyMetadata(null, OnSubtitleChanged));

	public static readonly DependencyProperty LeftContentProperty =
		DependencyProperty.Register(nameof(LeftContent), typeof(object), typeof(Window), new PropertyMetadata(null));

	public static readonly DependencyProperty RightContentProperty =
		DependencyProperty.Register(nameof(RightContent), typeof(object), typeof(Window), new PropertyMetadata(null));

	private static readonly DependencyProperty HasSubtitleProperty =
		DependencyProperty.Register(nameof(HasSubtitle), typeof(bool), typeof(Window), new PropertyMetadata(false));

	public string? Subtitle
	{
		get => (string?)GetValue(SubtitleProperty);
		set => SetValue(SubtitleProperty, value);
	}

	public object LeftContent
	{
		get => GetValue(LeftContentProperty);
		set => SetValue(LeftContentProperty, value);
	}

	public object RightContent
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

	private void OnWindowButtonClick(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is not Button btn)
		{
			return;
		}

		switch (btn.Name)
		{
			case "PART_MinimizeButton":
			WindowState = WindowState.Minimized;
			break;

			case "PART_MaximizeButton":
			WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
			break;

			case "PART_CloseButton":
			Close();
			break;
		}
	}

	private static void OnSubtitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is Window window)
		{
			window.HasSubtitle = !string.IsNullOrWhiteSpace(e.NewValue as string);
		}
	}
}
using System.Windows;

namespace FlintUI.Controls;

public class Button : System.Windows.Controls.Button
{
	static Button()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(typeof(Button)));
	}

	public static readonly DependencyProperty ButtonKindProperty =
		DependencyProperty.Register(nameof(ButtonKind), typeof(ButtonKind), typeof(Button), new PropertyMetadata(ButtonKind.Default));

	public static readonly DependencyProperty IconProperty =
		DependencyProperty.Register(nameof(Icon), typeof(IconKind), typeof(Button), new PropertyMetadata(IconKind.None, OnIconChanged));

	private static readonly DependencyPropertyKey HasIconPropertyKey =
		DependencyProperty.RegisterReadOnly(nameof(HasIcon), typeof(bool), typeof(Button), new PropertyMetadata(false));

	public static readonly DependencyProperty HasIconProperty = HasIconPropertyKey.DependencyProperty;

	public static readonly DependencyProperty IconSizeProperty =
		DependencyProperty.Register(nameof(IconSize), typeof(double), typeof(Button), new PropertyMetadata(13d));

	public static readonly DependencyProperty CornerRadiusProperty =
		DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Button), new PropertyMetadata(default(CornerRadius)));

	public ButtonKind ButtonKind
	{
		get => (ButtonKind)GetValue(ButtonKindProperty);
		set => SetValue(ButtonKindProperty, value);
	}

	public IconKind Icon
	{
		get => (IconKind)GetValue(IconProperty);
		set => SetValue(IconProperty, value);
	}

	public bool HasIcon =>
		(bool)GetValue(HasIconProperty);

	public double IconSize
	{
		get => (double)GetValue(IconSizeProperty);
		set => SetValue(IconSizeProperty, value);
	}

	public CornerRadius CornerRadius
	{
		get => (CornerRadius)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}

	private static void OnIconChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
	{
		var button = (Button)dependencyObject;
		var hasIcon = (IconKind)eventArgs.NewValue != IconKind.None;

		button.SetValue(HasIconPropertyKey, hasIcon);
	}
}

public enum ButtonKind
{
	Default,
	Accent,
	Danger,
	Ghost
}
using System.Windows;
using System.Windows.Controls;

namespace FlintUI.Controls;

public class Badge : ContentControl
{
	static Badge()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(Badge), new FrameworkPropertyMetadata(typeof(Badge)));
	}

	public static readonly DependencyProperty BadgeTypeProperty = DependencyProperty
		.Register(nameof(BadgeType), typeof(BadgeType), typeof(Badge), new PropertyMetadata(BadgeType.Default));

	public BadgeType BadgeType
	{
		get => (BadgeType)GetValue(BadgeTypeProperty);
		set => SetValue(BadgeTypeProperty, value);
	}
}

public enum BadgeType
{
	Default,
	Accent,
	Success,
	Warning,
	Danger
}
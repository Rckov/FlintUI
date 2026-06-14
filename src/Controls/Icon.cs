using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FlintUI.Controls;

public class Icon : Control
{
	static Icon()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(Icon), new FrameworkPropertyMetadata(typeof(Icon)));
	}

	public static readonly DependencyProperty KindProperty =
		DependencyProperty.Register(nameof(Kind), typeof(IconKind), typeof(Icon), new PropertyMetadata(IconKind.None, OnKindChanged));

	private static readonly DependencyPropertyKey DataPropertyKey =
		DependencyProperty.RegisterReadOnly(nameof(Data), typeof(Geometry), typeof(Icon), new PropertyMetadata());

	public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

	public static readonly DependencyProperty SizeProperty =
		DependencyProperty.Register(nameof(Size), typeof(double), typeof(Icon), new FrameworkPropertyMetadata(16d));

	public IconKind Kind
	{
		get => (IconKind)GetValue(KindProperty);
		set => SetValue(KindProperty, value);
	}

	public Geometry? Data
	{
		get => (Geometry?)GetValue(DataProperty);
		private set => SetValue(DataPropertyKey, value);
	}

	public double Size
	{
		get => (double)GetValue(SizeProperty);
		set => SetValue(SizeProperty, value);
	}

	private static void OnKindChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
	{
		if (dependencyObject is not Icon icon)
		{
			return;
		}

		icon.Data = ResolveGeometry((IconKind)args.NewValue);
	}

	private static Geometry? ResolveGeometry(IconKind kind)
	{
		if (kind == IconKind.None)
		{
			return null;
		}

		return Application.Current?.TryFindResource($"Icon.{kind}") as Geometry;
	}
}
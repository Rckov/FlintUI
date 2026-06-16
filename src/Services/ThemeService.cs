using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace FlintUI.Services;

/// <summary>
/// Applies themes at runtime by swapping the active brush dictionary in the application's merged resources.
/// </summary>
/// <param name="provider">The registry used to resolve a <see cref="ThemeType"/> to its brushes.</param>
public class ThemeService(ThemeProvider provider)
{
    /// <summary>
    /// The shared service instance backed by <see cref="ThemeProvider.Instance"/>.
    /// </summary>
    public static readonly ThemeService Instance = new(ThemeProvider.Instance);
    private ResourceDictionary? _current;

    /// <summary>
    /// The theme currently applied to the application.
    /// </summary>
    public ThemeType CurrentTheme { get; private set; }

    /// <summary>
    /// Applies a registered theme. Does nothing if the theme is already active.
    /// </summary>
    /// <param name="theme">The theme to apply.</param>
    /// <exception cref="InvalidOperationException">The theme is not registered with the provider.</exception>
    public void SetTheme(ThemeType theme)
    {
        var descriptor = provider.Get(theme) ??
                         throw new InvalidOperationException($"Theme not registered: {theme.Key}");

        if (CurrentTheme == theme)
        {
            return;
        }

        CurrentTheme = theme;
        Apply(descriptor);
    }

    /// <summary>
    /// Applies a theme from an explicit descriptor, bypassing the provider registry.
    /// </summary>
    /// <param name="theme">The descriptor whose brushes are applied.</param>
    public void SetTheme(ThemeDescriptor theme)
    {
        CurrentTheme = theme.Key;
        Apply(theme);
    }

    private void Apply(ThemeDescriptor descriptor)
    {
        var merged = Application.Current.Resources.MergedDictionaries;

        if (_current != null)
        {
            merged.Remove(_current);
        }

        _current = new ResourceDictionary
        {
            Source = new Uri(descriptor.BrushesUri, UriKind.Absolute)
        };

        merged.Add(_current);
    }
}

/// <summary>
/// Identifies a theme by a string key. <see cref="Light"/> and <see cref="Dark"/> are built in, and
/// applications can register custom keys for their own themes.
/// </summary>
/// <param name="key">The unique key that names the theme.</param>
/// <exception cref="ArgumentNullException"><paramref name="key"/> is <see langword="null"/>.</exception>
[TypeConverter(typeof(ThemeTypeConverter))]
public readonly struct ThemeType(string key) : IEquatable<ThemeType>
{
    /// <summary>
    /// The built-in dark theme.
    /// </summary>
    public static readonly ThemeType Dark = new("Dark");

    /// <summary>
    /// The built-in light theme.
    /// </summary>
    public static readonly ThemeType Light = new("Light");

    /// <summary>
    /// The key that names the theme.
    /// </summary>
    public string Key { get; } = key ?? throw new ArgumentNullException(nameof(key));

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ThemeType other && Equals(other);
    }

    /// <inheritdoc/>
    public bool Equals(ThemeType other)
    {
        return Key == other.Key;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    /// <summary>
    /// Returns the theme <see cref="Key"/>.
    /// </summary>
    /// <returns>The theme key.</returns>
    public override string ToString()
    {
        return Key;
    }

    /// <summary>
    /// Determines whether two themes have the same key.
    /// </summary>
    /// <param name="left">The first theme to compare.</param>
    /// <param name="right">The second theme to compare.</param>
    /// <returns><see langword="true"/> if the keys are equal; otherwise <see langword="false"/>.</returns>
    public static bool operator ==(ThemeType left, ThemeType right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Determines whether two themes have different keys.
    /// </summary>
    /// <param name="left">The first theme to compare.</param>
    /// <param name="right">The second theme to compare.</param>
    /// <returns><see langword="true"/> if the keys differ; otherwise <see langword="false"/>.</returns>
    public static bool operator !=(ThemeType left, ThemeType right)
    {
        return !left.Equals(right);
    }
}

/// <summary>
/// Converts strings to <see cref="ThemeType"/> values so themes can be set from XAML.
/// </summary>
#pragma warning disable CS8765 // Nullability mismatch across target frameworks
#pragma warning disable CS8604 // Possible null reference argument
public class ThemeTypeConverter : TypeConverter
{
    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object? value)
    {
        if (value is not string s)
        {
            return base.ConvertFrom(context, culture, value);
        }

        if (s == ThemeType.Light.Key)
        {
            return ThemeType.Light;
        }

        return s == ThemeType.Dark.Key ? ThemeType.Dark : new ThemeType(s);
    }
}
#pragma warning restore CS8604
#pragma warning restore CS8765
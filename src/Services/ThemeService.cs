using System.ComponentModel;
using System.Globalization;
using System.Windows;

namespace FlintUI.Services;

public class ThemeService(ThemeProvider provider)
{
    public static readonly ThemeService Instance = new(ThemeProvider.Instance);
    private ResourceDictionary? _current;
    public ThemeType CurrentTheme { get; private set; }

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

[TypeConverter(typeof(ThemeTypeConverter))]
public readonly struct ThemeType(string key) : IEquatable<ThemeType>
{
    public static readonly ThemeType Dark = new("Dark");
    public static readonly ThemeType Light = new("Light");

    public string Key { get; } = key ?? throw new ArgumentNullException(nameof(key));

    public override bool Equals(object? obj)
    {
        return obj is ThemeType other && Equals(other);
    }

    public bool Equals(ThemeType other)
    {
        return Key == other.Key;
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    public override string ToString()
    {
        return Key;
    }

    public static bool operator ==(ThemeType left, ThemeType right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ThemeType left, ThemeType right)
    {
        return !left.Equals(right);
    }
}

public class ThemeTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

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
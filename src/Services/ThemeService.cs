using System.Windows;
using FlintUI.Abstractions;

namespace FlintUI.Services;

public class ThemeService(ThemeProvider provider)
{
    private ResourceDictionary? _current;

    public static readonly ThemeService Instance = new(ThemeProvider.Instance);
    public ThemeType CurrentTheme { get; private set; }

    public void SetTheme(ThemeType theme)
    {
        var descriptor = provider.Get(theme) ?? throw new InvalidOperationException($"Theme not registered: {theme.Key}");

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

    public static bool operator == (ThemeType left, ThemeType right)
    {
        return left.Equals(right);
    }

    public static bool operator != (ThemeType left, ThemeType right)
    {
        return !left.Equals(right);
    }
}
namespace FlintUI.Services;

/// <summary>
/// Describes a theme by pairing its <see cref="ThemeType"/> key with the resource dictionary that holds its
/// brushes.
/// </summary>
/// <param name="key">The theme this descriptor applies to.</param>
/// <param name="brushesUri">An absolute pack URI to the resource dictionary that defines the theme's brushes.</param>
public class ThemeDescriptor(ThemeType key, string brushesUri)
{
    /// <summary>
    /// The theme this descriptor applies to.
    /// </summary>
    public ThemeType Key { get; } = key;

    /// <summary>
    /// The absolute pack URI of the resource dictionary that defines the theme's brushes.
    /// </summary>
    public string BrushesUri { get; } = brushesUri;
}

/// <summary>
/// A registry of the themes the application can switch between. Includes the built-in light and dark themes
/// and lets you register custom ones.
/// </summary>
public class ThemeProvider
{
    /// <summary>
    /// The shared registry instance used by the rest of the library.
    /// </summary>
    public static readonly ThemeProvider Instance = new();
    private readonly Dictionary<ThemeType, ThemeDescriptor> _descriptors = new();

    private ThemeProvider()
    {
        Register(new ThemeDescriptor(
            ThemeType.Light,
            "pack://application:,,,/FlintUI;component/Themes/Brushes/LightBrushes.xaml"));

        Register(new ThemeDescriptor(
            ThemeType.Dark,
            "pack://application:,,,/FlintUI;component/Themes/Brushes/DarkBrushes.xaml"));
    }

    /// <summary>
    /// The keys of all registered themes.
    /// </summary>
    public IReadOnlyCollection<ThemeType> Keys => _descriptors.Keys;

    /// <summary>
    /// Looks up the descriptor for a theme.
    /// </summary>
    /// <param name="type">The theme to look up.</param>
    /// <returns>The matching descriptor, or <see langword="null"/> if the theme is not registered.</returns>
    public ThemeDescriptor? Get(ThemeType type)
    {
        return _descriptors.TryGetValue(type, out var d) ? d : null;
    }

    /// <summary>
    /// Registers a theme, replacing any existing descriptor with the same <see cref="ThemeDescriptor.Key"/>.
    /// </summary>
    /// <param name="descriptor">The theme descriptor to register.</param>
    public void Register(ThemeDescriptor descriptor)
    {
        _descriptors[descriptor.Key] = descriptor;
    }
}
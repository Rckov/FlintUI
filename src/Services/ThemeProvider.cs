namespace FlintUI.Services;

public class ThemeDescriptor(ThemeType key, string brushesUri)
{
    public ThemeType Key { get; } = key;
    public string BrushesUri { get; } = brushesUri;
}

public class ThemeProvider
{
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

    public IReadOnlyCollection<ThemeType> Keys => _descriptors.Keys;

    public ThemeDescriptor? Get(ThemeType type)
    {
        return _descriptors.TryGetValue(type, out var d) ? d : null;
    }

    public void Register(ThemeDescriptor descriptor)
    {
        _descriptors[descriptor.Key] = descriptor;
    }
}
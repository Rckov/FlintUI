using System.Windows;
using FlintUI.Services;

namespace FlintUI;

/// <summary>
/// The resource dictionary that brings FlintUI into an application. Merge it into
/// <c>Application.Resources</c> to load the control styles and the selected theme.
/// </summary>
/// <remarks>
/// Setting <see cref="Theme"/> routes through the shared <see cref="ThemeService.Instance"/>, so the
/// chosen theme applies to the whole application, not just this dictionary instance.
/// </remarks>
public class FlintResources : ResourceDictionary
{
    /// <summary>
    /// Loads the FlintUI control styles and applies the <see cref="ThemeType.Light"/> theme by default.
    /// </summary>
    public FlintResources()
    {
        Theme = ThemeType.Light;
        MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/FlintUI;component/FlintResources.xaml", UriKind.Absolute)
        });
    }

    /// <summary>
    /// The theme applied across the application. Assigning a value switches the active brushes.
    /// </summary>
    public ThemeType Theme
    {
        get => ThemeService.Instance.CurrentTheme;
        set => ThemeService.Instance.SetTheme(value);
    }
}
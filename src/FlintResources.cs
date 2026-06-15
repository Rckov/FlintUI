using System.ComponentModel;
using System.Globalization;
using System.Windows;
using FlintUI.Abstractions;
using FlintUI.Services;

namespace FlintUI;

public class FlintResources : ResourceDictionary
{
    public FlintResources()
    {
        MergedDictionaries.Add(new ResourceDictionary
        {
            Source = new Uri("pack://application:,,,/FlintUI;component/FlintResources.xaml", UriKind.Absolute)
        });
    }

    public ThemeType Theme
    {
        get => ThemeService.Instance.CurrentTheme;
        set => ThemeService.Instance.SetTheme(value);
    }
}

using System.Windows;

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
}
using FlintUI.Controls;

namespace FlintUI.Example.Views;

public partial class IconsPage
{
    public IconsPage()
    {
        InitializeComponent();
        Icons.ItemsSource = Enum.GetValues<IconKind>().Where(kind => kind != IconKind.None).ToArray();
    }
}

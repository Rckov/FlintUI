using FlintUI.Controls;

namespace FlintUI.Example.Views;

public partial class IconsPage
{
    public IconsPage()
    {
        InitializeComponent();
        Icons.ItemsSource = Enum.GetValues(typeof(IconKind)).Cast<IconKind>().Where(kind => kind != IconKind.None);
    }
}
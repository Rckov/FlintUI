using FlintUI.Controls;

namespace FlintUI.Example.Views;

public partial class ShellView
{
    public ShellView()
    {
        InitializeComponent();

        Nav.ItemsSource = new[]
        {
            new GalleryPage("Buttons", IconKind.Zap, new ButtonsPage()),
            new GalleryPage("Inputs", IconKind.Edit, new InputsPage()),
            new GalleryPage("Selection", IconKind.CheckCircle, new SelectionPage()),
            new GalleryPage("Data", IconKind.Database, new DataPage()),
            new GalleryPage("Containers", IconKind.Layout, new ContainersPage()),
            new GalleryPage("Feedback", IconKind.Bell, new FeedbackPage()),
            new GalleryPage("Icons", IconKind.Grid, new IconsPage())
        };

        Nav.SelectedIndex = 0;
    }
}

public record GalleryPage(string Title, IconKind Icon, object View);

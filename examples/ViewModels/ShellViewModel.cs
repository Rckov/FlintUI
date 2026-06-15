using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlintUI.Example.Views;
using FlintUI.Services;

namespace FlintUI.Example.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty] private bool _isDarkTheme;
    [ObservableProperty] private string _searchText = string.Empty;

    [ObservableProperty] private DemoItem? _selectedItem;

    public ShellViewModel()
    {
        List<DemoItem> items =
        [
            new("Button", "Basic", new ButtonPage()),
            new("Badge", "Basic", new BadgePage()),
            new("Icon", "Basic", new IconsPage()),

            new("TextBox", "Inputs", new TextBoxPage()),
            new("PasswordBox", "Inputs", new PasswordBoxPage()),
            new("NumericUpDown", "Inputs", new NumericUpDownPage()),
            new("ComboBox", "Inputs", new ComboBoxPage()),
            new("DatePicker", "Inputs", new DatePickerPage()),
            new("Slider", "Inputs", new SliderPage()),

            new("CheckBox", "Selection", new CheckBoxPage()),
            new("RadioButton", "Selection", new RadioButtonPage()),
            new("ToggleSwitch", "Selection", new ToggleSwitchPage()),

            new("DataGrid", "Data", new DataGridPage()),
            new("ListBox", "Data", new ListBoxPage()),
            new("TreeView", "Data", new TreeViewPage()),
            new("ProgressBar", "Data", new ProgressBarPage()),

            new("TabControl", "Containers", new TabControlPage()),
            new("Expander", "Containers", new ExpanderPage()),
            new("GroupBox", "Containers", new GroupBoxPage()),
            new("Menu", "Containers", new MenuPage()),

            new("Dialog", "Feedback", new DialogPage()),
            new("Spinner", "Feedback", new SpinnerPage()),
            new("ToolTip", "Feedback", new ToolTipPage())
        ];

        Items = CollectionViewSource.GetDefaultView(items);
        Items.GroupDescriptions.Add(new PropertyGroupDescription(nameof(DemoItem.Category)));
        Items.Filter = Filter;

        SelectedItem = items[0];
    }

    public ICollectionView Items { get; }

    [RelayCommand]
    private void ChangeTheme()
    {
        var theme = ThemeService.Instance.CurrentTheme;

        ThemeService.Instance.SetTheme(theme != ThemeType.Dark
            ? ThemeType.Dark
            : ThemeType.Light);

        IsDarkTheme = theme == ThemeType.Dark;
    }

    [RelayCommand]
    private void OpenGitHub()
    {
        const string url = "https://github.com/Rckov/FlintUI";

        Process.Start(new ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }

    partial void OnSearchTextChanged(string value)
    {
        Items.Refresh();

        if (SelectedItem is null || !Filter(SelectedItem))
        {
            SelectedItem = Items.Cast<DemoItem>().FirstOrDefault();
        }
    }

    private bool Filter(object item)
    {
        return string.IsNullOrWhiteSpace(SearchText)
               || (item is DemoItem demo && demo.Title.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
    }
}

public class DemoItem(string title, string category, object view)
{
    public string Title { get; } = title;
    public string Category { get; } = category;
    public object View { get; } = view;
}
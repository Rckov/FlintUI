using System.Windows.Input;
using FlintUI.Controls;
using FlintUI.Example.ViewModels;

namespace FlintUI.Example.Views;

/// <summary>
///     Логика взаимодействия для MainView.xaml
/// </summary>
public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    private void OnShowDialog(object sender, MouseButtonEventArgs e)
    {
        Dialog.Show("Save changes before closing?", "Confirm", DialogButton.YesNoCancel, DialogIcon.Question, this);
    }
}
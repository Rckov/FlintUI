namespace FlintUI.Example.Views;

/// <summary>
/// Логика взаимодействия для MainView.xaml
/// </summary>
public partial class MainView : Controls.Window
{
	public MainView()
	{
		InitializeComponent();
		DataContext = new ViewModels.MainViewModel();
	}

	private void OnShowDialog(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		Controls.Dialog.Show("Save changes before closing?", "Confirm", Controls.DialogButton.YesNoCancel, Controls.DialogIcon.Question, this);
	}
}
using FlintUI.Example.ViewModels;

namespace FlintUI.Example.Views;

public partial class FramePage
{
    public FramePage()
    {
        InitializeComponent();

        var vm = new FrameViewModel();
        DataContext = vm;
        vm.NavigationRequested += page => DemoFrame.Navigate(page);
        vm.NavigateHomeCommand.Execute(null);
    }
}
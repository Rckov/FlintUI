using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlintUI.Example.Views;

namespace FlintUI.Example.ViewModels;

public partial class FrameViewModel : ObservableObject
{
    private readonly Stack<object> _back = new();
    private readonly Stack<object> _forward = new();
    
    private readonly FrameDemoHomePage _home = new();
    private readonly FrameDemoProfilePage _profile = new();
    private readonly FrameDemoSettingsPage _settings = new();
    private object? _current;

    public event Action<object>? NavigationRequested;

    [RelayCommand(CanExecute = nameof(HasBack))]
    private void GoBack()
    {
        _forward.Push(_current!);
        Navigate(_back.Pop(), false);
    }

    [RelayCommand(CanExecute = nameof(HasForward))]
    private void GoForward()
    {
        _back.Push(_current!);
        Navigate(_forward.Pop(), false);
    }

    [RelayCommand]
    private void NavigateHome()
    {
        Navigate(_home);
    }

    [RelayCommand]
    private void NavigateProfile()
    {
        Navigate(_profile);
    }

    [RelayCommand]
    private void NavigateSettings()
    {
        Navigate(_settings);
    }

    private void Navigate(object page, bool addToHistory = true)
    {
        if (addToHistory && _current is not null)
        {
            _back.Push(_current);
            _forward.Clear();
        }

        _current = page;
        NavigationRequested?.Invoke(page);

        GoBackCommand.NotifyCanExecuteChanged();
        GoForwardCommand.NotifyCanExecuteChanged();
    }

    private bool HasBack()
    {
        return _back.Count > 0;
    }

    private bool HasForward()
    {
        return _forward.Count > 0;
    }
}
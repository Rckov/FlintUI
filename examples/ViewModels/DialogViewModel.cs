using CommunityToolkit.Mvvm.Input;
using FlintUI.Abstractions;
using FlintUI.Services;

namespace FlintUI.Example.ViewModels;

public partial class DialogViewModel(IDialogService dialog)
{
    public DialogViewModel() : this(new DialogService())
    {
    }

    [RelayCommand]
    private void Info()
    {
        dialog.Info("Everything is up to date.", "Synced");
    }

    [RelayCommand]
    private void Success()
    {
        dialog.Success("Your changes have been saved.", "Saved");
    }

    [RelayCommand]
    private void Confirm()
    {
        dialog.Confirm("Delete this item? This action cannot be undone.", "Delete item");
    }

    [RelayCommand]
    private void Error()
    {
        dialog.Error("The connection to the server was lost.", "Connection error");
    }
}
using FlintUI.Controls;

namespace FlintUI.Abstractions;

public interface IDialogService
{
    DialogResult Show(string message, string? caption = null, DialogButton buttons = DialogButton.Ok, DialogIcon icon = DialogIcon.None);

    void Info(string message, string? caption = null);

    void Success(string message, string? caption = null);

    void Warning(string message, string? caption = null);

    void Error(string message, string? caption = null);

    bool Confirm(string message, string? caption = null);
}
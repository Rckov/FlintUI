using FlintUI.Abstractions;
using FlintUI.Controls;

namespace FlintUI.Services;

public class DialogService : IDialogService
{
    public DialogResult Show(string message, string? caption = null, DialogButton buttons = DialogButton.Ok,
        DialogIcon icon = DialogIcon.None)
    {
        return Dialog.Show(message, caption, buttons, icon);
    }

    public void Info(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Info);
    }

    public void Success(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Success);
    }

    public void Warning(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Warning);
    }

    public void Error(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Error);
    }

    public bool Confirm(string message, string? caption = null)
    {
        return Dialog.Show(message, caption, DialogButton.YesNo, DialogIcon.Question) == DialogResult.Yes;
    }
}
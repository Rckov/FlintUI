using FlintUI.Abstractions;
using FlintUI.Controls;

namespace FlintUI.Services;

/// <summary>
/// The default <see cref="IDialogService"/> implementation. Shows dialogs through the static
/// <see cref="Dialog"/> helper.
/// </summary>
public class DialogService : IDialogService
{
    /// <inheritdoc/>
    public DialogResult Show(string message, string? caption = null, DialogButton buttons = DialogButton.Ok,
        DialogIcon icon = DialogIcon.None)
    {
        return Dialog.Show(message, caption, buttons, icon);
    }

    /// <inheritdoc/>
    public void Info(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Info);
    }

    /// <inheritdoc/>
    public void Success(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Success);
    }

    /// <inheritdoc/>
    public void Warning(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Warning);
    }

    /// <inheritdoc/>
    public void Error(string message, string? caption = null)
    {
        Dialog.Show(message, caption, DialogButton.Ok, DialogIcon.Error);
    }

    /// <inheritdoc/>
    public bool Confirm(string message, string? caption = null)
    {
        return Dialog.Show(message, caption, DialogButton.YesNo, DialogIcon.Question) == DialogResult.Yes;
    }
}
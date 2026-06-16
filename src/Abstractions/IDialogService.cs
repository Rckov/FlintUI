using FlintUI.Controls;

namespace FlintUI.Abstractions;

/// <summary>
/// Lets view models request dialogs without referencing WPF types directly.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Shows a modal dialog with the given buttons and icon, and waits for the user to close it.
    /// </summary>
    /// <returns>The button the user clicked, or <see cref="DialogResult.None"/> if the dialog was dismissed.</returns>
    DialogResult Show(string message, string? caption = null, DialogButton buttons = DialogButton.Ok,
        DialogIcon icon = DialogIcon.None);

    /// <summary>
    /// Shows an informational dialog with an OK button.
    /// </summary>
    void Info(string message, string? caption = null);

    /// <summary>
    /// Shows a success dialog with an OK button.
    /// </summary>
    void Success(string message, string? caption = null);

    /// <summary>
    /// Shows a warning dialog with an OK button.
    /// </summary>
    void Warning(string message, string? caption = null);

    /// <summary>
    /// Shows an error dialog with an OK button.
    /// </summary>
    void Error(string message, string? caption = null);

    /// <summary>
    /// Shows a yes/no confirmation dialog. Returns <see langword="true"/> when the user clicks Yes.
    /// </summary>
    bool Confirm(string message, string? caption = null);
}
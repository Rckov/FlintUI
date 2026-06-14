using System.Windows;
using FlintUI.Controls;

namespace FlintUI.Example.Views;

public partial class FeedbackPage
{
    public FeedbackPage()
    {
        InitializeComponent();
    }

    private void OnInfo(object sender, RoutedEventArgs e)
    {
        Dialog.Show("Everything is up to date.", "Synced", DialogButton.Ok, DialogIcon.Info);
    }

    private void OnSuccess(object sender, RoutedEventArgs e)
    {
        Dialog.Show("Your changes have been saved.", "Saved", DialogButton.Ok, DialogIcon.Success);
    }

    private void OnConfirm(object sender, RoutedEventArgs e)
    {
        Dialog.Show("Delete this item? This action cannot be undone.", "Delete item", DialogButton.YesNoCancel,
            DialogIcon.Warning);
    }

    private void OnError(object sender, RoutedEventArgs e)
    {
        Dialog.Show("The connection to the server was lost.", "Connection error", DialogButton.Ok, DialogIcon.Error);
    }
}

using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace LetsTalk.Helpers
{
    public static class MessageDialogs
    {
        public const string Yes = "Yes";
        public const string No = "No";
        public const string Cancel = "Cancel";

        private static async Task<string> ShowYesNoCancelDialogAsync(string content, string title = "")
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand(Yes));
            dialog.Commands.Add(new UICommand(No));
            dialog.Commands.Add(new UICommand(Cancel));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 2;
            

            return (await dialog.ShowAsync()).Label;
        }
        private static async Task<string> ShowYesNoDialogAsync(string content, string title = "")
        {
            var dialog = new MessageDialog(content, title);
            dialog.Commands.Add(new UICommand(Yes));
            dialog.Commands.Add(new UICommand(No));
            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            return (await dialog.ShowAsync()).Label;
        }

        public static async Task<string> ShowSaveConfirmationDialogAsync()
        {
            return await ShowYesNoCancelDialogAsync("Do you want to save changes?", "Save Confirmation");
        }

        public static async Task<string> ShowExitConfirmationDialogAsync()
        {
            return await ShowYesNoDialogAsync("Do you want to exit?", "Exit Confirmation");
        }
    }
}

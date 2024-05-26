using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.Toolkit.Uwp.Notifications;
namespace LetsTalk.Services
{
    public class LetsTalkNotificationFactory
    {

        private const string FilNotFoundNotificationId = "FileNotFoundNotification";
        private const ToastDuration ErrorNotificationDuration = ToastDuration.Long;
        public static Task ShowFileNotFoundNotification(string filePath)
        {

            

            var notification = new ToastContentBuilder()
                .AddText($"Failed to find {Path.GetFileName(filePath)} at location {ApplicationData.Current.LocalFolder.Path}\\{Path.GetPathRoot(filePath)}!")
                .AddHeader(FilNotFoundNotificationId, "File not found", "")
                .SetToastDuration(ErrorNotificationDuration);

            notification.Show();



            return Task.CompletedTask;
        }
    }
}

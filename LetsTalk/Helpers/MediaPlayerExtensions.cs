using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Playback;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace LetsTalk.Helpers
{
    public static class MediaElementExtensions
    {

        public static async Task PlayStreamAsync(this MediaElement mediaElement,
            Windows.Storage.Streams.IRandomAccessStream stream, bool disposeStream = true)
        {
            TaskCompletionSource<bool> taskCompleted = new TaskCompletionSource<bool>();
            RoutedEventHandler endOfPlayerHandler = (s, e) =>
            {
                if (disposeStream)
                {
                    stream.Dispose();

                }

                taskCompleted.SetResult(true);
            };

            mediaElement.MediaEnded += endOfPlayerHandler;
            mediaElement.SetSource(stream, string.Empty);
            mediaElement.Play();

            await taskCompleted.Task;
            mediaElement.MediaEnded -= endOfPlayerHandler;

        }
    }
}

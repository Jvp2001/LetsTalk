using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace LetsTalk.ViewModels
{
    public class HelpViewModel : ObservableObject
    {
        // TODO: Set your default video and image URIs
        private const string DefaultSource =
            "https://sec.ch9.ms/ch9/db15/43c9fbed-535e-4013-8a4a-a74cc00adb15/C9L12WinTemplateStudio_high.mp4";

        // The poster image is displayed until the video is started
        private const string DefaultPoster =
            "https://sec.ch9.ms/ch9/db15/43c9fbed-535e-4013-8a4a-a74cc00adb15/C9L12WinTemplateStudio_960.jpg";

        private IMediaPlaybackSource source;

        public IMediaPlaybackSource Source
        {
            get => source;
            private set => SetProperty(ref source, value);
        }

        private string posterSource;

        public string PosterSource
        {
            get => posterSource;
            private set => SetProperty(ref posterSource, value);
        }

        public HelpViewModel()
        {
            Source = MediaSource.CreateFromUri(new Uri(DefaultSource));
            PosterSource = DefaultPoster;
        }

        public void DisposeSource()
        {
            var mediaSource = Source as MediaSource;
            mediaSource?.Dispose();
            Source = null;
        }
    }
}

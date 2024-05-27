using System;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace LetsTalk.ViewModels
{
    public class HelpViewModel : ObservableObject
    {
        private const string DefaultSource =
            "ms-appx:///Assets/Videos/TrainingVideo.mp4";

        // The poster image is displayed until the video is started
        private const string DefaultPoster =
            "ms-appx:///Assets/Images/TrainingVideoPoster.png";

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

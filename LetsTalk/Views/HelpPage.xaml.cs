using System;

using LetsTalk.ViewModels;

using Windows.Media.Playback;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LetsTalk.Views
{
    public sealed partial class HelpPage : Page
    {
        public HelpViewModel ViewModel { get; } = new HelpViewModel();

        // For more on the MediaPlayer and adjusting controls and behavior see https://docs.microsoft.com/windows/uwp/controls-and-patterns/media-playback
        // The DisplayRequest is used to stop the screen dimming while watching for extended periods
        private readonly DisplayRequest displayRequest = new DisplayRequest();
        private bool isRequestActive = false;

        public HelpPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            mpe.MediaPlayer.PlaybackSession.PlaybackStateChanged += PlaybackSession_PlaybackStateChanged;
            mpe.IsFullWindow = true;
            mpe.AutoPlay = ReferenceEquals(e.Parameter, "FirstTime");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            mpe.MediaPlayer.Pause();
            mpe.MediaPlayer.PlaybackSession.PlaybackStateChanged -= PlaybackSession_PlaybackStateChanged;
            ViewModel.DisposeSource();
        }

        private async void PlaybackSession_PlaybackStateChanged(MediaPlaybackSession sender, object args)
        {
            if (sender is null || sender.NaturalVideoHeight == 0)
            {
                return;
            }

            switch (sender.PlaybackState)
            {
                case MediaPlaybackState.Playing:
                {
                    if (!isRequestActive)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            displayRequest.RequestActive();
                            isRequestActive = true;
                        });
                    }

                    break;
                }
                default:
                {
                    if (isRequestActive)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            displayRequest.RequestRelease();
                            isRequestActive = false;
                        });
                    }

                    break;
                }
            }
        }
    }
}

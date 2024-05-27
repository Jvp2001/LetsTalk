using System;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using SpeechSynthesizer = Windows.Media.SpeechSynthesis.SpeechSynthesizer;


namespace LetsTalk.Services
{
    public class TextToSpeechService
    {

        private readonly SpeechSynthesizer speechSynthesiser = new SpeechSynthesizer();

        private void ApplySettings(MediaElement mediaElement)
        {
            speechSynthesiser.Options.SpeakingRate = LetsTalkSettingsService.TextToSpeechSettings.Rate;
            mediaElement.PlaybackRate = LetsTalkSettingsService.TextToSpeechSettings.Rate;
            mediaElement.Volume = LetsTalkSettingsService.TextToSpeechSettings.Volume / 100.0;
        }



        public async Task SpeakAsync(string text, MediaElement mediaElement)
        {

            ApplySettings(mediaElement);
            if (mediaElement.CurrentState == MediaElementState.Playing)
            {
                mediaElement.Stop();
            }





            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            try
            {
                SpeechSynthesisStream stream = await speechSynthesiser.SynthesizeTextToStreamAsync(text);
                mediaElement.AutoPlay = true;
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }
            catch (Exception)
            {
                mediaElement.AutoPlay = false;

                var messageDialog = new MessageDialog("Unable to synthesise");
                await messageDialog.ShowAsync();
            }

        }
    }
}

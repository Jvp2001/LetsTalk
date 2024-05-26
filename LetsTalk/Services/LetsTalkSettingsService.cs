using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using LetsTalk.Core.Models;
using LetsTalk.Helpers;

namespace LetsTalk.Services
{
    public static class LetsTalkSettingsService
    {
        private const string TextToSpeech= "TextToSpeech";
        private const string EyeTrackerKey = "EyeTracker";

        public static TextToSpeechSettings TextToSpeechSettings { get; set; } = new TextToSpeechSettings();
        public static EyeTrackerSettings EyeTrackerSettings { get; set; } = new EyeTrackerSettings();

    
        public static async Task InitialiseAsync()
        {
            await LoadSettingsAsync();
            TextToSpeechSettings.PropertyChanged += async (sender, e) => await SaveTextToSpeechSettingsAsync();
            EyeTrackerSettings.PropertyChanged += async (sender, e) => await SaveEyeTrackerSettingsAsync();
        }

        
        public static async Task LoadSettingsAsync()
        {
            await LoadTextToSpeechSettingsAsync();
            await LoadEyeTrackerSettingsAsync();
        }

        public static async Task SaveSettingsAsync()
        {
            await SaveTextToSpeechSettingsAsync();
            await SaveEyeTrackerSettingsAsync();
        }


        public static async Task SaveTextToSpeechSettingsAsync()
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(TextToSpeech, TextToSpeechSettings);
        }

        public
            static async Task LoadTextToSpeechSettingsAsync()
        {
            TextToSpeechSettings =
                (await ApplicationData.Current.LocalSettings.ReadAsync<TextToSpeechSettings>(TextToSpeech)) ?? new TextToSpeechSettings();

        }

        public static async Task SaveEyeTrackerSettingsAsync()
        {
            await ApplicationData.Current.LocalSettings.SaveAsync(EyeTrackerKey, EyeTrackerSettings);
        }

        public static async Task LoadEyeTrackerSettingsAsync()
        {
            EyeTrackerSettings =
                (await ApplicationData.Current.LocalSettings.ReadAsync<EyeTrackerSettings>(EyeTrackerKey)) ?? new EyeTrackerSettings();
        }
    }
}

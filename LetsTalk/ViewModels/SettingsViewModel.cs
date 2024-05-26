using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LetsTalk.Helpers;
using LetsTalk.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;
using Microsoft.UI.Xaml.Controls;

namespace LetsTalk.ViewModels
{
    // TODO: Add other settings as necessary. For help see https://github.com/microsoft/TemplateStudio/blob/main/docs/UWP/pages/settings.md
    public sealed class SettingsViewModel : ObservableObject
    {
        private ElementTheme elementTheme = ThemeSelectorService.Theme;

        public ElementTheme ElementTheme
        {
            get => elementTheme;

            set => SetProperty(ref elementTheme, value);
        }

        private string versionDescription;

        public string VersionDescription
        {
            get => versionDescription;

            set => SetProperty(ref versionDescription, value);
        }

        private ICommand switchThemeCommand;

        public ICommand SwitchThemeCommand
        {
            get
            {
                if (switchThemeCommand == null)
                {
                    switchThemeCommand = new RelayCommand<ElementTheme>(
                        async (param) =>
                        {
                            ElementTheme = param;
                            await ThemeSelectorService.SetThemeAsync(param);
                        });
                }

                return switchThemeCommand;
            }
        }

        public SettingsViewModel()
        {
        }

        public async Task InitializeAsync()
        {
            VersionDescription = GetVersionDescription();
            await Task.CompletedTask;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        public async Task OnDwellDurationChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
            LetsTalkSettingsService.EyeTrackerSettings.DwellDuration = (int)sender.Value;
            await LetsTalkSettingsService.SaveSettingsAsync();
            
        }

        public void OnCursorRadiusChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
        {
        }
    }
}

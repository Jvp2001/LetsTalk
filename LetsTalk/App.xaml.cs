using LetsTalk.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using LetsTalk.Core.Helpers;
using LetsTalk.Database;
using LetsTalk.Models;

namespace LetsTalk
{
    public sealed class AppProperties : INotifyPropertyChanged
    {
        private UserType user;

        public UserType User
        {
            get => user;
            set => SetField(ref user, value);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public sealed partial class App : Application
    {
        private readonly Lazy<ActivationService> activationService;

        public ActivationService ActivationService => activationService.Value;

        public CardModelDatabase CardModelDatabase => Singleton<CardModelDatabase>.Instance;

        /// <summary>
        /// This redefines the Current property to the type of this class. This is done to avoid casting the Current property every time it is used.
        /// </summary>
        public static new App Current => Application.Current as App;


        public CustomCardBoardDatabase CardBoardDatabase => Singleton<CustomCardBoardDatabase>.Instance;
        public MyBoardsDatabase MyBoardsDatabase => Singleton<MyBoardsDatabase>.Instance;
        public TextToSpeechService TextToSpeechService => Singleton<TextToSpeechService>.Instance;


        public AppProperties AppProperties => Singleton<AppProperties>.Instance;




        // This is just here, so I do not have to rewrite code to use the App.Current.User property.
        public UserType User
        {
            get => AppProperties.User;
            set => AppProperties.User = value;
        }

        public const uint MaximumNumberOfCardsPerBord = 9;

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
                "Ngo9BigBOggjHTQxAR8/V1NBaF1cXmhMYVB3WmFZfVpgfF9EYVZQQ2YuP1ZhSXxXdkBhXX9WcnVUQWBbV0Q=");

            activationService = new Lazy<ActivationService>(() => CreateActivationService());
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.WelcomePage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}

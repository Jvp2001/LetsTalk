using LetsTalk.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using LetsTalk.Core.Contracts.Services;
using LetsTalk.Core.Services;
using AutoRegisterSourceGen;

namespace LetsTalk
{
    [
        AutoRegisterPages("Page"),
        AutoRegisterViewModels("ViewModel")
    ]
    public sealed partial class App : Application
    {
        private ActivationService ActivationService
        {
            get { return GetService<ActivationService>(); }
        }

        public IHost Host { get; private set; }

        private IServiceCollection serviceProvider;

        public static T GetService<T>() where T : class
        {
            T service = (App.Current as App).Host.Services.GetService(typeof(T)) as T;
            if (service is null)
            {
                throw new ArgumentException(
                    $"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs");
            }

            return service;
        }


        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.

            serviceProvider = new ServiceCollection()
                .AddSingleton(_ => CreateActivationService())
                .AddSingleton<IFileService, FileService>();
           

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
            return new ActivationService(this, typeof(Views.MainMenuPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}

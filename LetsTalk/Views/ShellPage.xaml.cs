using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Devices.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using LetsTalk.Helpers;
using LetsTalk.Services;

namespace LetsTalk.Views
{
    // TODO: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();


        private CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
            CoreApplication.GetCurrentView().CoreWindow.KeyUp += CoreWindow_KeyUp;
         

        }

        
        
        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case VirtualKey.XButton1:
                    if (shellFrame.CanGoBack)
                    {
                        shellFrame.GoBack();
                    }
                    break;
        
                case VirtualKey.XButton2:
                    if (shellFrame.CanGoForward)
                    {
                        shellFrame.GoForward();
                    }
                    break;
        
            }
        }

        private void ShellFrame_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                // Back
                case VirtualKey.XButton1:
                    if (shellFrame.CanGoBack)
                    {
                        shellFrame.GoBack();
                    }

                    break;
                // Forward
                case VirtualKey.XButton2:
                    if (shellFrame.CanGoForward)
                    {
                        shellFrame.GoForward();
                    }

                    break;

                default:
                    break;
            }
        }

        private async void UIElement_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {

            switch (await MessageDialogs.ShowExitConfirmationDialogAsync())
            {
                case MessageDialogs.Yes:
                    await ApplicationView.GetForCurrentView().TryConsolidateAsync();
                    break;
                case MessageDialogs.No:
                    break;

            }
        }
    }
}

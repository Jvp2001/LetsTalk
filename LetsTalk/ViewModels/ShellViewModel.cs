using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using LetsTalk.Helpers;
using LetsTalk.Services;
using LetsTalk.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Contracts.ViewModels;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace LetsTalk.ViewModels
{
    public sealed class ShellViewModel : ObservableObject, INavigationAware
    {
        private readonly KeyboardAccelerator altLeftKeyboardAccelerator =
            BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu);

        private readonly KeyboardAccelerator backKeyboardAccelerator = BuildKeyboardAccelerator(VirtualKey.GoBack);


        private bool isBackEnabled;
        private IList<KeyboardAccelerator> keyboardAccelerators;
        private WinUI.NavigationView navigationView;
        private WinUI.NavigationViewItem selected;
        private ICommand loadedCommand;
        private ICommand itemInvokedCommand;
        private bool isPaneVisible;
        private WinUI.NavigationViewPaneDisplayMode paneDisplayMode;


        public bool IsBackEnabled
        {
            get => isBackEnabled;
            set => SetProperty(ref isBackEnabled, value);
        }

        public WinUI.NavigationViewItem Selected
        {
            get => selected;
            set => SetProperty(ref selected, value);
        }

        public ICommand LoadedCommand => loadedCommand ?? (loadedCommand = new RelayCommand(OnLoaded));

        public ICommand ItemInvokedCommand => itemInvokedCommand ??
                                              (itemInvokedCommand =
                                                  new RelayCommand<WinUI.NavigationViewItemInvokedEventArgs>(
                                                      OnItemInvoked));

        public bool IsPaneVisible
        {
            get => isPaneVisible;
            set
            {
                if (value == isPaneVisible) return;
                isPaneVisible = value;
                OnPropertyChanged();
            }
        }

        public WinUI.NavigationViewPaneDisplayMode PaneDisplayMode
        {
            get => paneDisplayMode;
            set
            {
                if (value == paneDisplayMode) return;
                paneDisplayMode = value;
                OnPropertyChanged();
            }
        }

        public ShellViewModel()
        {
        NavigationService.Navigating += (sender, args) =>
        {
            var wasOnWelcomePage = args.SourcePageType != typeof(WelcomePage);
            IsPaneVisible =  wasOnWelcomePage;
            PaneDisplayMode = wasOnWelcomePage
                ? WinUI.NavigationViewPaneDisplayMode.LeftCompact
                : WinUI.NavigationViewPaneDisplayMode.Auto;
        
        
        };

                
        }

        public void Initialize(Frame frame, WinUI.NavigationView navigationView,
            IList<KeyboardAccelerator> accelerators)
        {
            this.navigationView = navigationView;
            this.keyboardAccelerators = accelerators;
            NavigationService.Frame = frame;
            NavigationService.NavigationFailed += Frame_NavigationFailed;
            NavigationService.Navigated += Frame_Navigated;
            this.navigationView.BackRequested += OnBackRequested;
        }

        private async void OnLoaded()
        {
            // KeyBoard accelerators are added here to avoid showing 'Alt + left' tooltip on the page.
            // More info on tracking issue https://github.com/Microsoft/microsoft-ui-xaml/issues/8
            keyboardAccelerators.Add(altLeftKeyboardAccelerator);
            keyboardAccelerators.Add(backKeyboardAccelerator);

            await Task.CompletedTask;
        }

        private void OnItemInvoked(WinUI.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                NavigationService.Navigate(typeof(SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else
            {
                var selectedItem = args.InvokedItemContainer as WinUI.NavigationViewItem;
                var pageType = selectedItem?.GetValue(NavHelper.NavigateToProperty) as Type;

                if (!(pageType is null))
                {
                    NavigationService.Navigate(pageType, null, args.RecommendedNavigationTransitionInfo);
                }
            }
        }

        private void OnBackRequested(WinUI.NavigationView sender, WinUI.NavigationViewBackRequestedEventArgs args)
        {
            
            NavigationService.GoBack();
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw e.Exception;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            IsBackEnabled = NavigationService.CanGoBack;
            if (e.SourcePageType == typeof(SettingsPage))
            {
                Selected = navigationView.SettingsItem as WinUI.NavigationViewItem;
                return;
            }

            var selectedItem = GetSelectedItem(navigationView.MenuItems, e.SourcePageType);
            if (!(selectedItem  is null))
            {
                Selected = selectedItem;
            }
        }

        private WinUI.NavigationViewItem GetSelectedItem(IEnumerable<object> menuItems, Type pageType)
        {
            foreach (var item in menuItems.OfType<WinUI.NavigationViewItem>())
            {
                if (IsMenuItemForPageType(item, pageType))
                {
                    return item;
                }

                var selectedChild = GetSelectedItem(item.MenuItems, pageType);
                if (selectedChild != null)
                {
                    return selectedChild;
                }
            }

            return null;
        }

        private bool IsMenuItemForPageType(WinUI.NavigationViewItem menuItem, Type sourcePageType)
        {
            var pageType = menuItem.GetValue(NavHelper.NavigateToProperty) as Type;
            return pageType == sourcePageType;
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key,
            VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerators = new KeyboardAccelerator() { Key = key };
            if (modifiers.HasValue)
            {
                keyboardAccelerators.Modifiers = modifiers.Value;
            }

            keyboardAccelerators.Invoked += OnKeyBoardAcceleratorInvoked;
            return keyboardAccelerators;
        }

        private static void OnKeyBoardAcceleratorInvoked(KeyboardAccelerator sender,
            KeyboardAcceleratorInvokedEventArgs args)
        {
                if (NavigationService.CanGoBack)
                {
                    args.Handled = NavigationService.GoBack();
                }
        }




        //INavigationAware implementation
        public void OnNavigatedTo(object parameter)
        {
            if (NavigationService.Frame.SourcePageType != typeof(WelcomePage))
            {
                PaneDisplayMode = WinUI.NavigationViewPaneDisplayMode.LeftCompact;
                IsPaneVisible = true;
            }
        }

        public Task OnNavigatedFrom()
        {
            return Task.CompletedTask;
        }
    }
}

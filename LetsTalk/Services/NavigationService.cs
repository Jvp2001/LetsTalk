using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Contracts.ViewModels;
using LetsTalk.Helpers;

namespace LetsTalk.Services
{
    public static class NavigationService
    {
        public static event NavigatingCancelEventHandler Navigating;
        public static event NavigatedEventHandler Navigated;

        public static event NavigationFailedEventHandler NavigationFailed;

        public static event Action GoBackButtonPressed;
        private static Frame frame;
        private static object lastParamUsed;

        private static Dictionary<string, string> navigationStates = new Dictionary<string, string>();

        public static Frame Frame
        {
            get
            {
                if (frame == null)
                {
                    frame = Window.Current.Content as Frame;
                    RegisterFrameEvents();
                }

                return frame;
            }

            set
            {
                UnregisterFrameEvents();
                frame = value;
                RegisterFrameEvents();
            }
        }

        // CanGoBack and previous page is not WelcomePage
        public static bool CanGoBack => Frame.CanGoBack &&
                                        Frame.BackStack[Frame.BackStackDepth - 1].SourcePageType !=
                                        typeof(LetsTalk.Views.WelcomePage) && Frame.BackStackDepth != 1;

        public static bool CanGoForward => Frame.CanGoForward;

        public static bool GoBack()
        {
            object vmBeforeNavigation = Frame.GetPageViewModel();

            if(vmBeforeNavigation is IBackButtonClickedHandler backButtonClickedHandler)
            {
                backButtonClickedHandler.OnBackButtonClicked();
                return true;
            }
            if (CanGoBack)
            {
                Frame.GoBack();
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }

                return true;
            }

            return false;
        }

        public static void GoForward()
        {
            Frame.GoForward();
        }

        public static bool Navigate(Type pageType, object parameter = null,
            NavigationTransitionInfo infoOverride = null)
        {
            if (pageType is null || !pageType.IsSubclassOf(typeof(Page)))
            {
                throw new ArgumentException($"Invalid pageType '{pageType}', please provide a valid pageType.",
                    nameof(pageType));
            }

            // Don't open the same page multiple times
            if (Frame.Content?.GetType() != pageType || (!(parameter is null) && !parameter.Equals(lastParamUsed)))
            {
                var navigationResult = Frame.Navigate(pageType, parameter, infoOverride);
                if (navigationResult)
                {
                    lastParamUsed = parameter;
                }

                return navigationResult;
            }
            else
            {
                return false;
            }
        }

        public static bool Navigate<T>(object parameter = null, NavigationTransitionInfo infoOverride = null)
            where T : Page
        {
            return Navigate(typeof(T), parameter, infoOverride);
        }



        public static void RemoveLastEntry()
        {
            if (Frame.BackStackDepth > 0)
            {
                Frame.BackStack.RemoveAt(Frame.BackStackDepth - 1);
            }
        }



        private static void RegisterFrameEvents()
        {
            if (!(frame is null))
            {
                frame.Navigating += Frame_Navigating;
                frame.Navigated += Frame_Navigated;
                frame.NavigationFailed += Frame_NavigationFailed;
            }
        }

        private static void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {

            if (sender is Frame senderFrame)
            {
                var viewModel = senderFrame.GetPageViewModel();
                if (viewModel is null)
                {
                    goto InvokeNavigating;
                }
                if (viewModel is INavigatingHandler navigatingHandler)
                {
                    navigatingHandler.OnNavigatingFrom(e);
                }
            }

            InvokeNavigating:
            Navigating?.Invoke(sender, e);
        }

        private static void UnregisterFrameEvents()
        {
            if (!(frame is null))
            {
                frame.Navigating -= Frame_Navigating;
                frame.Navigated -= Frame_Navigated;
                frame.NavigationFailed -= Frame_NavigationFailed;
            }
        }

        private static void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            NavigationFailed?.Invoke(sender, e);
        }

        private static void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame senderFrame)
            {


                if (senderFrame.GetPageViewModel() is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.Parameter);
                }
            }

            Navigated?.Invoke(sender, e);
        }
    }
}

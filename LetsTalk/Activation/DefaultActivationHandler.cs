using System;
using System.Threading.Tasks;
using LetsTalk.Services;
using Windows.ApplicationModel.Activation;

namespace LetsTalk.Activation
{
    internal class DefaultActivationHandler : ActivationHandler<IActivatedEventArgs>
    {
        private readonly Type navElement;

        public DefaultActivationHandler(Type navElement)
        {
            this.navElement = navElement;
        }

        protected async override Task HandleInternalAsync(IActivatedEventArgs args)
        {
            // When the navigation stack isn't restored, navigate to the first page and configure
            // the new page by passing required information in the navigation parameter
            object arguments = null;
            if (args is LaunchActivatedEventArgs launchArgs)
            {
                arguments = launchArgs.Arguments;
            }

            NavigationService.Navigate(navElement, arguments);

         
            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(IActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
            return NavigationService.Frame.Content is null && !(navElement is null);
        }
    }
}

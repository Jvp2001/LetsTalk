using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Controls;

namespace LetsTalk.Contracts.Services
{
    public interface INavigationService
    {
        event NavigatedEventHandler Navigated;

        bool CanGoBack
        {
            get;
        }

        Frame  Frame
        {
            get; set;
        }

        bool NavigateTo(string pageKey, object  parameter = null, bool clearNavigation = false);

        bool GoBack();
    }
}

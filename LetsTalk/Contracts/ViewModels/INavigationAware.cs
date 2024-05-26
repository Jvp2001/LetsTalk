using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace LetsTalk.Contracts.ViewModels
{
    /// <summary>
    /// This allows for ViewModels to be notified of navigation events.
    /// </summary>
    ///<remarks>
    /// <see cref="LetsTalk.Services.NavigationService"/> for how the interface is used.
    /// </remarks>
    public interface INavigationAware
    {
        /// <summary>
        /// This is called when the Page is navigated to.
        /// </summary>
        /// <param name="parameter"></param>
        void OnNavigatedTo(object parameter);

        /// <summary>
        /// This is called when the Page is navigated from.
        /// </summary>
        /// <returns> A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task OnNavigatedFrom();
    }

    /// <summary>
    /// Allows for conditional based navigation.
    ///</summary>
    /// <remarks>
    /// This is in a separate interface to <see cref="INavigationAware"/> becuase the C# version the project is using does not allow for optional interface methods (C# 8).
    /// </remarks>
    public interface INavigatingHandler
    {

        /// <summary>
        /// This is called when the Page is navigating away from.
        /// This allows a ViewModel to cancel the navigation, by setting the <param name="navigatingCancelEventArgs"/> Cancel property to true.
        /// </summary>
        /// <param name="navigatingCancelEventArgs"> The <see cref="NavigatingCancelEventArgs"/> instance containing the event data.</param>
        /// <returns> A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task OnNavigatingFrom(NavigatingCancelEventArgs navigatingCancelEventArgs);

    }
}

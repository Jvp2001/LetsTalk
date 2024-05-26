using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Contracts.ViewModels;
using LetsTalk.Models;
using LetsTalk.Services;
using LetsTalk.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace LetsTalk.ViewModels
{
    public class MainMenuViewModel : ObservableObject, INavigationAware
    {
        public MainMenuViewModel()
        {

        }

        public void ChildButton_Clicked(object sender, RoutedEventArgs e)
        {
            App.Current.User = UserType.Child;
            NavigationService.Navigate<SampleBoardsPage>();


        }

        public async void OnNavigatedTo(object parameter)
        {
            await App.Current.ActivationService.StartupAsync();
        }

        public Task OnNavigatedFrom()
        {
            return Task.CompletedTask;
        }
    }
}

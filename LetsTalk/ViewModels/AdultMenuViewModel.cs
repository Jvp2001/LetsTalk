using System;
using Windows.UI.Xaml;
using LetsTalk.Services;
using LetsTalk.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using LetsTalk.Models;
using Windows.UI.Xaml.Documents;

namespace LetsTalk.ViewModels
{
    public sealed class AdultMenuViewModel : ObservableObject
    {
        public AdultMenuViewModel()
        {
        }

        public void OnSampleBoardsClicked(object sender, RoutedEventArgs e)
        {
            App.Current.User = UserType.Adult;
             NavigationService.Navigate<SampleBoardsPage>();
        }
    }
}

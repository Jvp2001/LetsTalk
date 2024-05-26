using System;
using System.Linq;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Models;

namespace LetsTalk.Views
{
    public sealed partial class AdultMenuPage
    {
        public AdultMenuViewModel ViewModel { get; } = new AdultMenuViewModel();

        public AdultMenuPage()
        {
            InitializeComponent();
        }




        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.Current.User = Frame.BackStack.LastOrDefault()?.GetType() == typeof(MainMenuPage)
                ? UserType.Adult
                : App.Current.User;

            base.OnNavigatedFrom(e);

        }
    }
}

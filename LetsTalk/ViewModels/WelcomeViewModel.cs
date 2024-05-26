using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LetsTalk.Services;
using LetsTalk.Views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace LetsTalk.ViewModels
{
    public class WelcomeViewModel : ObservableObject
    {

        public event Action Dismissed;
        private readonly DispatcherTimer displayTimer;
        public WelcomeViewModel()
        {
            displayTimer = new DispatcherTimer();
            displayTimer.Interval = TimeSpan.FromSeconds(5);
            displayTimer.Tick += DisplayTimer_Tick;
            displayTimer.Start();
            
        }



        private void DisplayTimer_Tick(object sender, object e)
        {
            displayTimer.Stop();
            NavigationService.Navigate<MainMenuPage>();
            
            

        }
    }
}

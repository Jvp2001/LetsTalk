using System;

using LetsTalk.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class WelcomePage : Page
    {
        public WelcomeViewModel ViewModel { get; } = new WelcomeViewModel();

        public WelcomePage()
        {
            InitializeComponent();
        }
    }
}

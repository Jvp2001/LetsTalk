using System;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class MainMenuPage : Page
    {
        public MainMenuViewModel ViewModel { get; } = new MainMenuViewModel();

        public MainMenuPage()
        {
            InitializeComponent();
        }
    }
}

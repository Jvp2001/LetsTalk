using System;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class AdultMenuPage : Page
    {
        public AdultMenuViewModel ViewModel { get; } = new AdultMenuViewModel();

        public AdultMenuPage()
        {
            InitializeComponent();
        }
    }
}

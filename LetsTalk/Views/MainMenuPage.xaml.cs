using System;
using Windows.UI.Xaml;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;
using LetsTalk.Services;
using Microsoft.Toolkit.Uwp.Input.GazeInteraction;

namespace LetsTalk.Views
{
    public sealed partial class MainMenuPage
    {
        public MainMenuViewModel ViewModel { get; } = new MainMenuViewModel();

        public MainMenuPage()
        {
            InitializeComponent();




        }


    }
}


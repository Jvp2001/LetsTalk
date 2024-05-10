using System;

using LetsTalk.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class SampleBoardsPage : Page
    {
        public SampleBoardsViewModel ViewModel { get; } = new SampleBoardsViewModel();

        public SampleBoardsPage()
        {
            InitializeComponent();
        }
    }
}

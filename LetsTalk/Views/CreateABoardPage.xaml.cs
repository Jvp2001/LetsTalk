using System;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class CreateABoardPage : Page
    {
        public CreateABoardViewModel ViewModel { get; } = new CreateABoardViewModel();

        public CreateABoardPage()
        {
            InitializeComponent();
        }
    }
}

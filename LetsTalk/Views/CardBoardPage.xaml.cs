using System;

using LetsTalk.ViewModels;

using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class CardBoardPage : Page
    {
        public CardBoardViewModel ViewModel { get; } = new CardBoardViewModel();

        public CardBoardPage()
        {
            InitializeComponent();
        }
    }
}

using System;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class SymbolWorkshopPage : Page
    {
        public SymbolWorkshopViewModel ViewModel { get; } = new SymbolWorkshopViewModel();

        public SymbolWorkshopPage()
        {
            InitializeComponent();
        }
    }
}

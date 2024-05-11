using System;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;

namespace LetsTalk.Views
{
    public sealed partial class ChildMenuPage : Page
    {
        public ChildMenuViewModel ViewModel { get; } = new ChildMenuViewModel();

        public ChildMenuPage()
        {
            InitializeComponent();
        }
    }
}

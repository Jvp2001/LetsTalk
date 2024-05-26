using System;
using Windows.UI.Xaml;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Models;
using LetsTalk.Services;
using System.Linq;
using Windows.UI.Xaml.Input;

namespace LetsTalk.Views
{
    public sealed partial class SampleBoardsPage : Page
    {



        private const string BoardNameId = "BoardName";

        public SampleBoardsViewModel ViewModel { get; } = new SampleBoardsViewModel();

        public SampleBoardsPage()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ViewModel.Boards.Count == 0)
            {
                await ViewModel.GetBoardsAsync();
            }
        }


        private void SampleBoards_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                var templateRoot = args.ItemContainer.ContentTemplateRoot as  Grid;

                if (!(templateRoot?.FindName(BoardNameId) is TextBlock textBlock))
                {
                    return;
                }

                textBlock.ClearValue(TextBlock.TextProperty);
            }


            if (args.Phase == 0)
            {
                args.RegisterUpdateCallback(ShowBoardName);
            }
        }

        private async void ShowBoardName(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase == 1)
            {
                var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;

                if (!(templateRoot?.FindName(BoardNameId) is TextBlock textBlock))
                {
                    return;
                }



                var board = args.Item as CardBoardModel;
                var name = board?.Name ?? "";
                textBlock.Text = name.Contains("_") ? name.Replace("_", " ") : name;
                textBlock.TextAlignment = TextAlignment.Center;
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            }

        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}

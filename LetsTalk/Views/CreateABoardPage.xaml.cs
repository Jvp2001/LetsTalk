using System;
using System.Linq;
using LetsTalk.Models;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Services;

namespace LetsTalk.Views
{
    public sealed partial class CreateABoardPage : Page
    {
        public CreateABoardViewModel ViewModel { get; } = new CreateABoardViewModel();

        public CreateABoardPage()
        {
            InitializeComponent();



        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationService.Frame.BackStack.Last().SourcePageType == typeof(AdultMenuPage))
            {

                var Dialog = ViewModel.Dialog;
                var Board = ViewModel.Board;
                var BoardName = ViewModel.BoardName;
                var result = await Dialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    Board.Name = Dialog.BoardName;
                    BoardName = Board.Name;
                    BoardNameTextBlock.Text = Dialog.BoardName;
                }
                else
                {
                    NavigationService.Navigate<AdultMenuPage>();
                }
            }
        }

        private void CardBoard_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                if (!(args.ItemContainer.ContentTemplateRoot is Grid templateRoot))
                {
                    return;
                }

                if (!(templateRoot.FindName("CardImage") is Image image))
                {
                    return;
                }

                image.Source = null;
            }

            if (args.Phase == 0)
            {
                args.RegisterUpdateCallback(ShowImage);
                args.Handled = true;
            }
        }

        private async void ShowImage(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase == 1)
            {
                var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;

                if (!(templateRoot?.FindName("CardImage") is Image image))
                {
                    return;
                }

                image.Opacity = 100;

                var card = (CardFileModel)args.Item;

                try
                {
                    if (card is null)
                        throw new NullReferenceException();
                    image.Source = await card.GetImageThumbnailAsync();
                }
                catch (Exception)
                {
                    var bitmapImage = new BitmapImage
                    {
                        UriSource = new Uri("Assets/StoreLogo.png")
                    };
                    image.Source = bitmapImage;
                }
            }
        }
    }
}

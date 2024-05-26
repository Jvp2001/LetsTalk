using System;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Models;

namespace LetsTalk.Views
{

    /// <summary>
    /// The default purpose of this pages is to show you a list of cards that the application has. It also lets you add your own cards.
    ///
    /// <remarks>
    /// If navigating from the <see cref="CreateABoardPage"/> or the <see cref="BoardPage"/>, then when a card is clicked it will update the cell on that Board.
    /// </remarks>
    /// </summary>
    public sealed partial class SymbolWorkshopPage
    {
        public SymbolWorkshopViewModel ViewModel { get; } = new SymbolWorkshopViewModel();

        public SymbolWorkshopPage()
        {
            InitializeComponent();
        }

  protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is EditBoardCellModel editBoardCellModel)
            {
            }
            if (!ViewModel.HaveAnyCards)
            {
                await ViewModel.GetCardsAsync();
            }
            base.OnNavigatedTo(e);
        }


        private void CardBoard_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.CardBoard_ItemClicked(sender, e);
        }

        //Q: What does the event do?

        private void CardBoard_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.InRecycleQueue)
            {
                var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
                if (templateRoot is null)
                {
                    return;
                }

                var image = templateRoot.FindName("CardImage") as Image;
                if (image is null)
                {
                    return;
                }

                image.Source = null;
            }

            if (args.Phase == 0)
            {
                args.RegisterUpdateCallback(ShowCard);
                args.Handled = true;
            }
        }
        private async void ShowCard(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (args.Phase == 1)
            {
                var templateRoot = args.ItemContainer.ContentTemplateRoot as Grid;
                if (templateRoot is null)
                {
                    return;
                }

                var image = templateRoot.FindName("CardImage") as Image;

                if (image is null)
                {
                    return;
                }

                image.Opacity = 100;

                var card = args.Item as CardFileModel;

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

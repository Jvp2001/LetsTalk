using System;
using Windows.UI.Xaml;
using LetsTalk.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Models;
using LetsTalk.Services;
using LetsTalk.Views.Extensions;

namespace LetsTalk.Views
{
    public sealed partial class BoardPage : Page, IPDFExportablePage
    {
        public BoardViewModel ViewModel { get; } = new BoardViewModel();

        public UIElement Element => CardBoard;

        private IPDFExportablePage PdfExportable => this;


        public BoardPage()
        {
            InitializeComponent();
            ViewModel.MediaElement = MediaElement;



        }

        private void CardBoard_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {

            ShowCard(sender, args);
        }



        private async void ShowCard(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            // if (args.Phase == 1)
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
                    BitmapImage imageThumbnailAsync = await card.GetImageThumbnailAsync();

                    image.Source = imageThumbnailAsync;
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


        private async void Export(object sender, RoutedEventArgs e)
        {

            PdfExportable.SaveAndExport(ViewModel.Board.Name ?? "");
        }



        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            // ViewModel.Board = SampleBoardsViewModel.CurrentBoard;



            switch (e.Parameter)
            {
                case Type type when type == typeof(SampleBoardsPage):
                    ViewModel.Board = SampleBoardsViewModel.CurrentBoard;

                    await ViewModel.GetCardsAsync();
                    break;
                case EditBoardCellModel editBoardCellModel:
                    ViewModel.Board = editBoardCellModel.CurrentBoard;
                    ViewModel.Board.Cards[editBoardCellModel.Index] = editBoardCellModel.Card;
                    await ViewModel.Board.SetupCards();
                    ViewModel.HasChanged = true;



                    // This removes the SymbolWorkshopPage for the backstack, so that the user cannot navigate back to it.
                    // The reason I did this was that the page is only used as a temporary page for selecting the new card image.
                    NavigationService.RemoveLastEntry();
                    break;
            }
        }







    }
}

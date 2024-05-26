using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
using LetsTalk.Contracts.ViewModels;
using LetsTalk.Database;
using LetsTalk.Helpers;
using LetsTalk.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using LetsTalk.Services;
using LetsTalk.Views;
using Windows.UI.Xaml.Controls;

namespace LetsTalk.ViewModels
{
    public sealed class SampleBoardsViewModel : ObservableObject, INavigationAware
    {
        private string titleText;

        private CustomCardBoardDatabase Database { get; set; } = App.Current.CardBoardDatabase;

        public ObservableCollection<CardBoardModel> Boards { get; set; } = new ObservableCollection<CardBoardModel>();


        public static CardBoardModel CurrentBoard { get; set; }


        public string TitleText
        {
            get => titleText;
            set => SetProperty(ref titleText, value);
        }

        public SampleBoardsViewModel()
        {
        }


        internal void SampleBoards_ItemClicked(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is CardBoardModel board)
            {
                CurrentBoard = board;
                    NavigationService.Navigate<BoardPage>(typeof(SampleBoardsPage));
            }
        }


        /// <summary>
        /// A helper method to load all the default card boards from the Assets folder.
        /// </summary>
        /// <remarks>
        /// This is only used if there are no boards in the database.
        /// </remarks>
        /// <returns></returns>
        private async Task<ObservableCollection<CardBoardModel>> LoadCardBoardsAsync()
        {
            StorageFolder cardsFolder =
                await Package.Current.InstalledLocation.GetFolderAsync("Assets\\Images\\Cards\\Colour");
            ObservableCollection<CardBoardModel> boards = new ObservableCollection<CardBoardModel>();
            foreach (var cardFolder in await cardsFolder.GetFoldersAsync())
            {
                var cardBoard = new CardBoardModel(cardFolder.Name);

                foreach (StorageFile cardFile in await cardFolder.GetFilesAsync(CommonFileQuery.DefaultQuery, 0,  App.MaximumNumberOfCardsPerBord))

                {
                    var card = new CardFileModel
                    {
                        Title = cardFile.DisplayName,
                        Image = cardFile.Path
                    };
                    await card.SetupCardAsync();
                    cardBoard.Cards.Add(card);
                }

                boards.Add(cardBoard);
            }

            return boards;
        }

        public async Task GetBoardsAsync()
        {
            var loadedBoards =
                await Database.Get() ??
                new ObservableCollection<CardBoardModel>();

            if (loadedBoards.Count == 0)
            {
                Boards = await LoadCardBoardsAsync();
                await Database.UpdateAllAsync(Boards, CancellationToken.None);
            }
            else
            {
                foreach (var board in loadedBoards)
                {
                    Boards.Add(board);
                }

                await Database.UpdateAllUniqueAsync(Boards, CancellationToken.None);
            }

            if (App.Current.User == UserType.Adult)
            {
                Boards.AddRange(await App.Current.MyBoardsDatabase.Get());
            }
        }


        #region INavigationAware Implementation

        public void OnNavigatedTo(object parameter)
        {
            switch (App.Current.User)
            {
                case UserType.Adult:
                    Database = App.Current.CardBoardDatabase;
                    break;
                case UserType.Child:
                    TitleText = "My Boards";
                    Database = App.Current.MyBoardsDatabase;
                    break;
            }
        }

        public Task OnNavigatedFrom()
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}

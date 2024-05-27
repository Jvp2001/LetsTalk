using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml.Controls;
using LetsTalk.Helpers;
using LetsTalk.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace LetsTalk.ViewModels
{

    /// <summary>
    /// A base class for view models that display a collection of cards.
    /// </summary>
    public abstract class CardBoardViewModel : ObservableObject
    {
        private bool isItemClickedEnabled = true;

        public ObservableCollection<CardFileModel> Cards { get; } = new ObservableCollection<CardFileModel>();


        public bool IsItemClickedEnabled
        {
            get => isItemClickedEnabled;
            set => SetProperty(ref isItemClickedEnabled, value);
        }

        public bool HaveAnyCards => Cards.Count > 0;




        protected CardBoardViewModel()
        {

        }

        protected async Task<IReadOnlyList<StorageFile>> LoadCardsAsync()
        {
            QueryOptions queryOptions = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".jpg", ".png" })
            {
                FolderDepth = FolderDepth.Deep
            };

            var appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var assetsFolder = await appInstalledFolder.GetFolderAsync("Assets\\Images\\Cards\\Colour");

            return await assetsFolder.CreateFileQueryWithOptions(queryOptions).GetFilesAsync();
        }


        /// <summary>
        /// This is the default behaviour of how cards are loaded.
        /// </summary>
        /// <remarks>
        /// This is used by the <see cref="SymbolWorkshopViewModel"/> to load the cards from the Assets folder.
        /// </remarks>
        public virtual async Task GetCardsAsync()
        {

            var result = await LoadCardsAsync();
            var loadedCards = new ObservableCollection<CardFileModel>();

            for (var index = 0; index < result.Count; ++index)
            {
                var file = result[index];
                var card = new CardFileModel
                {
                    Title = file.DisplayName,
                    Image = file.Path
                };
                await card.SetupCardAsync();
                Cards.Add(card);
            }

            OnCardsLoaded();

        }

        public async Task<CardFileModel> LoadImageFromStorageFileAsync(StorageFile file)
        {
            var properties = await file.Properties.GetImagePropertiesAsync();
            return new CardFileModel(file.DisplayName, file.Path, properties, file);
        }
        protected virtual void OnCardsLoaded()
        {

        }

        public async void CardBoard_ItemClicked(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is CardFileModel card)
            {
                // Do something with the card
                await OnCardClicked(card);
            }
        }

        protected virtual Task OnCardClicked(CardFileModel card)
        {
            // Do something with the card
            return Task.CompletedTask;
        }

    }
}

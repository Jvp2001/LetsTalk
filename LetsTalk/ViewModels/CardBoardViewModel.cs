using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Windows.UI.Xaml;
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
        public ObservableCollection<CardFileModel> Cards { get; set; } = new ObservableCollection<CardFileModel>();


        public bool IsItemClickedEnabled    
        {
            get => isItemClickedEnabled;
            set => SetProperty(ref isItemClickedEnabled, value);
        }

        public bool HaveAnyCards => Cards.Count > 0;

        protected virtual QueryOptions QueryOptions
        {
            get
            {
                var options = new QueryOptions(CommonFileQuery.DefaultQuery, new[] { ".jpg", ".png" })
                {
                    FolderDepth = FolderDepth.Deep
                };
                return options;
            }
        }






        public CardBoardViewModel()
        {

        }

        protected virtual async Task<IReadOnlyList<StorageFile>> LoadCardsAsync()
        {
            var options = QueryOptions;

            StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Assets\\Images\\Cards\\Colour");

            return await assetsFolder.CreateFileQueryWithOptions(options).GetFilesAsync();
        }


        public virtual async Task GetCardsAsync()
        {

            var result = await LoadCardsAsync();
            var loadedCards = new ObservableCollection<CardFileModel>();
            
            for (var index = 0; index < result.Count; index++)
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
        public async Task<StorageFile> LoadImageFileAsync(string path)
        {
            return await StorageFile.GetFileFromPathAsync(path);
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

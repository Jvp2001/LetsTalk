using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AsyncAwaitBestPractices.MVVM;
using LetsTalk.Contracts.ViewModels;
using LetsTalk.Core.Helpers;
using LetsTalk.Core.Models;
using LetsTalk.Database;
using LetsTalk.Helpers;
using LetsTalk.Models;
using LetsTalk.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace LetsTalk.ViewModels
{
    public sealed class SymbolWorkshopViewModel : CardBoardViewModel, INavigationAware
    {


        public IAsyncCommand OpenImagesPickerCommand { get; }

        public IAsyncCommand OpenImageFolderPickerCommand { get; }

        public bool ItemClickIsEnabled { get; set; }

        private EditBoardCellModel editBoardCellModel;

        public SymbolWorkshopViewModel()
        {
            OpenImagesPickerCommand = new AsyncCommand(OpenImagesPicker);
            OpenImageFolderPickerCommand = new AsyncCommand(OpenImageFolderPicker);
        }


        public async override Task GetCardsAsync()
        {




            var loadedCards = await App.Current.CardModelDatabase.Get() ?? new ObservableCollection<CardFileModel>();

            if(loadedCards.Count == 0)
            {
                await base.GetCardsAsync();
                await App.Current.CardModelDatabase.UpdateAllUniqueAsync(Cards, CancellationToken.None);

            }
            else
            {
                foreach (var card in loadedCards)
                {
                    Cards.Add(card);
                }

                await App.Current.CardModelDatabase.UpdateAllUniqueAsync(Cards, CancellationToken.None);
            }
        }

        public async Task OpenImagesPicker()
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                FileTypeFilter = { ".jpg", ".jpeg", ".png" }
            };


            var result = await fileOpenPicker.PickMultipleFilesAsync();
            if (result is null)
            {
                return;
            }

            for (var i = 0; i < result.Count; ++i)
            {
                var file = result[i];
                Cards.Add(await LoadImageFromStorageFileAsync(file));
            }
           await  App.Current.CardModelDatabase.UpdateAllUniqueAsync(Cards, CancellationToken.None);


        }

        public async Task OpenImageFolderPicker()
        {
            var folderPicker = new FolderPicker
            {
                CommitButtonText = "Pick Card Image Folders",
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            var result = await folderPicker.PickSingleFolderAsync();
            if (result is null)
            {
                return;
            }
            result.CreateFileQuery(CommonFileQuery.DefaultQuery);
            var folders = await result.GetFoldersAsync();
            for (var i = 0; i < folders.Count; i++)
            {
                var folder = folders[i];
                var files = await folder.GetFilesAsync();
                for (var index = 0; index < files.Count; index++)
                {
                    var file = files[index];
                    Cards.Add(await LoadImageFromStorageFileAsync(file));
                }
            }
            await App.Current.CardModelDatabase.UpdateAllUniqueAsync(Cards, CancellationToken.None);


        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is EditBoardCellModel model)
            {

                editBoardCellModel = model;
                ItemClickIsEnabled = true;

            }
        }

        public Task OnNavigatedFrom()
        {
            return Task.CompletedTask;
        }

        protected override async Task OnCardClicked(CardFileModel card)
        {


            if (editBoardCellModel is null)
            {
                return;
            }
            var index = editBoardCellModel.Index;
            if (card is  null)
            {
                return;
            }

            editBoardCellModel.CurrentBoard.Cards[index].Image = card.Image;
            await editBoardCellModel.CurrentBoard.SetupCards();

            NavigationService.Navigate(editBoardCellModel.PageType, editBoardCellModel);
        }
    }


}

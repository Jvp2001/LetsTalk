using LetsTalk.Database;
using LetsTalk.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Contracts.ViewModels;
using LetsTalk.Helpers;
using LetsTalk.Services;
using LetsTalk.Views;

namespace LetsTalk.ViewModels
{


    // To help reduce code duplication.The IBoardModificationViewModel interface was created to define the properties and methods that are common to all classes that implement it.
    public interface IBoardModificationViewModel
    {

        bool ShowBoardNameDialog { get; }
        CardBoardModel Board { get; set; }
        BoardNameDialog Dialog { get; }

        DatabaseBase<CardBoardModel> Database { get; }

    }

    /// <summary>
    /// This is an extension class that contains methods that are used to save, save as, and rename a board.
    /// </summary>
    public static class BoardModificationViewModelExtensions
    {

        public static async Task Save(this IBoardModificationViewModel viewModel, RoutedEventArgs e)
        {
            if (viewModel.ShowBoardNameDialog)
            {
                await viewModel.Save();
            }
            else
            {
                await viewModel.Database.UpdateAsync(viewModel.Board, CancellationToken.None);
            }
        }
        public static async Task Save(this IBoardModificationViewModel viewModel)
        {

            await viewModel.HandleBoardNameDialog("Save", async boardName =>
            {
                viewModel.Board.Name = boardName;
                await viewModel.Database.UpdateAsync(viewModel.Board, CancellationToken.None);
            });
        }

        public static async Task SaveAs(this IBoardModificationViewModel viewModel, RoutedEventArgs e)
        {
            await viewModel.SaveAs();
        }
        public static async Task SaveAs(this IBoardModificationViewModel viewModel)
        {

            await viewModel.HandleBoardNameDialog("Save As...", async boardName =>
            {
                viewModel.Board.Name = boardName;
                await viewModel.Database.UpdateAsync(viewModel.Board, CancellationToken.None);
            });
            await viewModel.Database.InsertAsync(viewModel.Board, CancellationToken.None);
        }

        public static async Task Rename(this IBoardModificationViewModel viewModel, RoutedEventArgs e)
        {

            await viewModel.Database.DropTableAsync(viewModel.Board);
            await viewModel.HandleBoardNameDialog("Rename", async boardName =>
            {
                viewModel.Board.Name = boardName;
                await viewModel.Database.UpdateAsync(viewModel.Board, CancellationToken.None);
            });
        }


        private static async Task HandleBoardNameDialog(this IBoardModificationViewModel viewModel, string title, Func<string, Task> primaryAction)

        {
            viewModel.Dialog.Title = title;
            var result = await viewModel.Dialog.ShowAsync();
            switch (result)
            {
                case ContentDialogResult.Primary:
                    await primaryAction(viewModel.Dialog.BoardName);
                    break;
                case ContentDialogResult.Secondary:
                    break;
            }
        }

    }



    public abstract class BoardModificationViewModel : ObservableObject, IBoardModificationViewModel, INavigatingHandler, INavigationAware
    {
        public abstract DatabaseBase<CardBoardModel> Database { get; private protected set; }


        public bool ShowBoardNameDialog => Board.Name == "";
        public CardBoardModel Board { get; set; } = new CardBoardModel("");
        public BoardNameDialog Dialog { get; }



        private IBoardModificationViewModel BoardModificationViewModelInterface => this;

        private protected bool HasChanged { get; set; }



        private bool NeedsSaving => HasChanged || Board.Name == "";



        protected BoardModificationViewModel()
        {
            Dialog = new BoardNameDialog(Board);
        }


        public ObservableCollection<CardFileModel> Cards => Board.Cards;


        public virtual Task OnNavigatedFrom()
        {
            return Task.CompletedTask;
        }

        public virtual void OnNavigatedTo(object parameter)
        {


        }

        public Task OnNavigatingFrom(NavigatingCancelEventArgs args)
        {
            // if (NeedsSaving && args.SourcePageType != typeof(SymbolWorkshopPage))
            // {
            //     // The navigate function cannot be called outside the switch statement as it will run before the user has made a choice, due to being asynchronous.
            //     switch (await MessageDialogs.ShowSaveConfirmationDialogAsync() )
            //     {
            //         case MessageDialogs.Yes:
            //
            //             await Database.UpdateAsync(Board, CancellationToken.None);
            //             NavigationService.Navigate<AdultMenuPage>();
            //             break;
            //         case MessageDialogs.No:
            //             NavigationService.Navigate<AdultMenuPage>();
            //             break;
            //         case MessageDialogs.Cancel:
            //             args.Cancel = true;
            //             break;
            //     }
            // }
            // else
            // {
            //     NavigationService.Navigate<SampleBoardsPage>();
            // }

            return Task.CompletedTask;

        }
    }
}

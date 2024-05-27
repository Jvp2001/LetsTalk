// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using Windows.Media.SpeechSynthesis;
// using Windows.UI.Xaml;
// using Windows.UI.Xaml.Controls;
// using Windows.UI.Xaml.Navigation;
// using LetsTalk.Contracts.ViewModels;
// using LetsTalk.Database;
// using LetsTalk.Helpers;
// using LetsTalk.Models;
// using LetsTalk.Services;
// using LetsTalk.Views;
// using Microsoft.Toolkit.Mvvm.ComponentModel;
//
// namespace LetsTalk.ViewModels
// {
//     public sealed class BoardViewModel : CardBoardViewModel, INavigationAware, INavigatingHandler, IBoardModificationViewModel
//     {
//         #region IBoardModificationViewModel Properties Implementation
//
//         public BoardNameDialog Dialog { get; }
//         public DatabaseBase<CardBoardModel> Database { get; private protected set; }
//         public bool ShowBoardNameDialog => true;
//
//         #endregion
//
//
//         public bool HasChanged
//         {
//             get;
//             set;
//         }
//         private string boardName;
//         private bool NeedsSaving => HasChanged || Board.Name == "";
//
//         public string TitleText => App.Current.User == UserType.Adult ? "" : "My Boards";
//
//
//         public string BoardName
//         {
//             get => boardName;
//             set => SetProperty(ref boardName, value);
//         }
//
//
//         public CardBoardModel Board { get; set; }
//         public MediaElement MediaElement { get; set; }
//
//
//         public BoardViewModel()
//         {
//             Board = new CardBoardModel("");
//             Database = App.Current.MyBoardsDatabase;
//             Dialog = new BoardNameDialog(Board);
//         }
//
//
//         public override Task GetCardsAsync()
//         {
//
//             for (var index = 0; index < App.MaximumNumberOfCardsPerBord; index++)
//             {
//
//
//                 // Prevents the index from exceeding the number of cards in the board, as some boards might not have the maximum number of cards. This also stop an IndexOutOfRangeException.
//                 if (index >= Board.Cards.Count)
//                 {
//                     break;
//                 }
//                 CardFileModel cardFileModel = Board.Cards[index];
//                 Cards.Add(cardFileModel);
//             }
//
//             return Task.CompletedTask;
//         }
//
//
//         protected override async Task OnCardClicked(CardFileModel card)
//         {
//             IsItemClickedEnabled = false;
//             switch (App.Current.User)
//             {
//                 case UserType.Adult:
//
//                     var editBoardCellModel = new EditBoardCellModel
//                     {
//                         Card = card,
//                         Index = Board.IndexOf(card),
//                         CurrentBoard = Board,
//                         PageType = typeof(BoardPage)
//                     };
//                     NavigationService.Navigate<SymbolWorkshopPage>(editBoardCellModel);
//                     break;
//                 case UserType.Child:
//                     await App.Current.TextToSpeechService.SpeakAsync(card.Title, MediaElement);
//                     break;
//                 default:
//                     throw new ArgumentOutOfRangeException();
//             }
//         }
//
//
//         public void MediaElement_OnMediaOpened(object sender, RoutedEventArgs e)
//         {
//             IsItemClickedEnabled = false;
//         }
//
//         public void MediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
//         {
//             IsItemClickedEnabled = true;
//         }
//
//         #region IBoardModificationViewModel Extension wrappers
//
// // These methods are wrappers for the IBoardModificationViewModel interface extension methods, which is implemented by this class.
//         public async void Save(object sender, RoutedEventArgs e)
//         {
//             await ((IBoardModificationViewModel)this).Save(sender, e);
//         }
//
//         public async void SaveAs(object sender, RoutedEventArgs e)
//         {
//             await ((IBoardModificationViewModel)this).SaveAs(sender, e);
//         }
//
//         public async void Rename(object sender, RoutedEventArgs e)
//         {
//             await ((IBoardModificationViewModel)this).Rename(sender, e);
//         }
//
//         #endregion
//
//
//
//         #region INavigatingHandler Implementation
//
//         public async Task OnNavigatingFrom(NavigatingCancelEventArgs args)
//         {
//
//             if (NeedsSaving)
//             {
//                 // The navigate function cannot be called outside the switch statement as it will run before the user has made a choice, due to being asynchronous.
//                 switch (await MessageDialogs.ShowSaveConfirmationDialogAsync())
//                 {
//                     case MessageDialogs.Yes:
//                         await Database.UpdateAsync(Board, CancellationToken.None);
//                         NavigationService.Navigate<SampleBoardsPage>();
//                         break;
//                     case MessageDialogs.No:
//                         NavigationService.Navigate<SampleBoardsPage>();
//                         break;
//                     case MessageDialogs.Cancel:
//                         args.Cancel = true;
//                         break;
//                 }
//             }
//             else
//             {
//                 NavigationService.Navigate<SampleBoardsPage>();
//             }
//
//         }
//
//
//         #endregion
//
//         #region INavigationAware Implementation
//
//
//         public async void OnNavigatedTo(object parameter)
//         {
//
//
//         }
//
//
//         public Task OnNavigatedFrom()
//         {
//            return Task.FromResult(Task.CompletedTask);
//         }
// #endregion
//     }
// }


using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LetsTalk.Contracts.ViewModels;
using LetsTalk.Database;
using LetsTalk.Helpers;
using LetsTalk.Models;
using LetsTalk.Services;
using LetsTalk.Views.Controls;

namespace LetsTalk.ViewModels
{
    public sealed class BoardViewModel : CardBoardViewModel, INavigationAware, INavigatingHandler, IBoardModificationViewModel
    {


        private IBoardModificationViewModel BoardModificationViewModel => this;

        #region IBoardModificationViewModel Properties Implementation

        public BoardNameDialog Dialog { get; }

        public DatabaseBase<CardBoardModel> Database { get; private protected set; }

        public bool ShowBoardNameDialog => true;
        public MediaElement MediaElement
        {
            get;
            set;
        }
        public CardBoardModel Board
        {
            get;

            set;
        }
        public bool HasChanged
        {
            get;

            set;
        }
        public string BoardName
        {
            get;
            set;
        } = "";

        #endregion


        public void OnNavigatedTo(object parameter)
        {

        }
        public Task OnNavigatedFrom()
        {
            return Task.FromResult(Task.CompletedTask);
        }
        public Task OnNavigatingFrom(NavigatingCancelEventArgs navigatingCancelEventArgs)
        {
            return Task.FromResult(Task.CompletedTask);
        }


        public void MediaElement_OnMediaOpened(object sender, RoutedEventArgs e)
         {
             IsItemClickedEnabled = false;
         }

         public void MediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
         {
             IsItemClickedEnabled = true;
         }


         public async void Save(object sender, RoutedEventArgs e)
         {
             await BoardModificationViewModel.Save(e);
         }

         public async void SaveAs(object sender, RoutedEventArgs e)
         {
             await BoardModificationViewModel.SaveAs(e);
         }

         public async void Rename(object sender, RoutedEventArgs e)
         {
             await BoardModificationViewModel.Rename(e);
         }

         public async override Task GetCardsAsync()
         {

             for (var index = 0; index < App.MaximumNumberOfCardsPerBord; index++)
             {


                 // Prevents the index from exceeding the number of cards in the board, as some boards might not have the maximum number of cards. This also stop an IndexOutOfRangeException.
                 if (index >= Board.Cards.Count)
                 {
                     break;
                 }
                 CardFileModel cardFileModel = Board.Cards[index];
                 Cards.Add(cardFileModel);
             }

             await Cards.SetupCardsAsync();

         }


         protected async override Task OnCardClicked(CardFileModel card)
         {
             await App.Current.TextToSpeechService.SpeakAsync(card.Title, MediaElement);

         }



    }
}

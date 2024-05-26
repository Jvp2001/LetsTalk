using System;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LetsTalk.Contracts.ViewModels;
using LetsTalk.Database;
using LetsTalk.Helpers;
using LetsTalk.Models;
using LetsTalk.Services;
using LetsTalk.Views;

namespace LetsTalk.ViewModels
{
    public sealed class CreateABoardViewModel : BoardModificationViewModel, IBackButtonClickedHandler
    {
        private int numberOfColumns = 1;
        private int numberOfRows = 1;

        private double errorTextBlockOpacity;

        private const string BothZeroErrorMessage = "Both rows and columns cannot be set to zero!";
        private const string OutOfRangeErrorMessage = "Rows and columns must be between 0 and 3!";
        private string errorMessage = "";
        private bool textBoxIsValid;
        private int selectedCardIndex;
        private string boardName = "Test";





        public string ErrorTextBlockText
        {
            get => errorMessage;
            private set => SetProperty(ref errorMessage, value);
        }


        public int NumberOfColumns
        {
            get => numberOfColumns;
            set => SetProperty(ref numberOfColumns, value);
        }

        public int NumberOfRows

        {
            get => numberOfRows;
            set => SetProperty(ref numberOfRows, value);
        }

        public override DatabaseBase<CardBoardModel> Database { get; private protected set; } = App.Current.MyBoardsDatabase;

        public double ErrorTextBlockOpacity
        {
            get => errorTextBlockOpacity;
            private set => SetProperty(ref errorTextBlockOpacity, value);
        }

        public bool TextBoxIsValid
        {
            get => textBoxIsValid;
            set => SetProperty(ref textBoxIsValid, value);
        }

        public int MaximumNumberOfColumns => 3;

        public int MaximumNumberOfRows => 3;

        public int SelectedCardIndex
        {
            get => selectedCardIndex;

            set => SetProperty(ref selectedCardIndex, value);
        }


        public string BoardName
        {
            get => boardName;

            set => SetProperty(ref boardName, value);
        }


        private void GenerateBoard()
        {
            if (TextBoxIsValid)
            {
                var oldCards = Cards.ToImmutableHashSet();
                Cards.Clear();
                var newSize = NumberOfColumns * NumberOfRows;
                // newSize is less than old cards count, add as many cards as needed from the old cards
                // if newSize is greater than old cards count, add all the oldCards plus the difference

                if (newSize < oldCards.Count)
                {
                    foreach (var card in oldCards.Take(newSize))
                    {
                        Cards.Add(card);
                    }
                }
                else
                {
                    Cards.AddRange(oldCards);
                    for (var i = oldCards.Count; i < newSize; i++)
                    {
                        Cards.Add(new CardFileModel());
                    }
                }
            }
        }

        public void OnCardClicked(object sender, ItemClickEventArgs e)
        {
            var cardFileModel = (CardFileModel)e.ClickedItem;
            var index = Board.IndexOf(cardFileModel);
            NavigationService.Navigate<SymbolWorkshopPage>(new EditBoardCellModel
            {
                Card = cardFileModel,

                Rows = NumberOfRows, Columns = NumberOfColumns,
                Index = index,
                PageType = typeof(CreateABoardPage),
                CurrentBoard = Board
            });
        }


        private void ValidateTextBoxes()
        {
            if (NumberOfRows == 0 && NumberOfColumns == 0)
            {
                ErrorTextBlockText = BothZeroErrorMessage;
            }
            else if (NumberOfRows > MaximumNumberOfRows || NumberOfColumns > MaximumNumberOfColumns)
            {
                ErrorTextBlockText = OutOfRangeErrorMessage;
            }
            else
            {

                ErrorTextBlockText = "";
            }
        }



        public void OnBackButtonClicked()
        {
            NavigationService.Navigate<AdultMenuPage>();
        }

        public async override void OnNavigatedTo(object parameter)
        {


            if (parameter is EditBoardCellModel model)
            {

                var row = model.Row;
                var column = model.Column;
                NumberOfRows = model.Rows;
                NumberOfColumns = model.Columns;
                Board = model.CurrentBoard;
                // Cards[row * NumberOfColumns + column] = model.Card;

                HasChanged = true;
                await Database.UpdateAsync(Board, CancellationToken.None);

                NavigationService.RemoveLastEntry();

            }
        }




        #region IBoardModificationViewModel Extension wrappers

        // These methods are wrappers for the IBoardModificationViewModel interface extension methods, which is implemented by this class.
        public async void Save(object sender, RoutedEventArgs e)
        {
            await this.Save(e);

        }

        public async void SaveAs(object sender, RoutedEventArgs e)
        {
            await this.SaveAs(e);
        }

        public async void Rename(object sender, RoutedEventArgs e)
        {
            await this.Rename(e);
        }

        #endregion


        public async void CreateABoardTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateTextBoxes();
            TextBoxIsValid = ErrorTextBlockText == "";
            GenerateBoard();
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {


            if (e.PropertyName == nameof(ErrorTextBlockText))
            {
                TextBoxIsValid = ErrorTextBlockText == "";
                ErrorTextBlockOpacity = ErrorTextBlockText.Length > 0 ? 1 : 0;
            }


            if (e.PropertyName == nameof(NumberOfColumns) || e.PropertyName == nameof(NumberOfRows))
            {


            }


        }

    }
}

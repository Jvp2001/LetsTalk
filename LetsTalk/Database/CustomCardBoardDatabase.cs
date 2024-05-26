using System;
using System.Collections;
using LetsTalk.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using LetsTalk.Helpers;

namespace LetsTalk.Database
{
    //TODO: Use the for all Board saving.
    public class CustomCardBoardDatabase : DatabaseBase<CardBoardModel>
    {
       private protected readonly string DatabaseFolderPath = $@"{DatabaseFolderName}\Boards";

        public override string DatabasePath => DatabaseFolderPath;

        
        public override bool IsEachModelItsOwnTable => true;


        public CustomCardBoardDatabase()
        {
            ApplicationData.Current.LocalFolder.CreateFolderAsync(DatabaseFolderPath);
        }


        protected override async Task OnDataFetched(IEnumerable<CardBoardModel> data)
        {
            foreach (var cardBoard in data)
            {
                await cardBoard.SetupCards();
            }
        }
    }
}

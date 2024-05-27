using LetsTalk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace LetsTalk.Database
{
    /// <summary>
    /// Used to save pre-made sample boards to the database.
    /// </summary>
    public class CardBoardDatabase : DatabaseBase<CardBoardModel>
    {
       private protected readonly string DatabaseFolderPath = $@"{DatabaseFolderName}\Boards";

        public override string DatabasePath => DatabaseFolderPath;

        
        public override bool IsEachModelItsOwnTable => true;


        public CardBoardDatabase()
        {
            ApplicationData.Current.LocalFolder.CreateFolderAsync(DatabaseFolderPath);
        }


        protected virtual async Task OnDataFetched(IEnumerable<CardBoardModel> data)
        {
            foreach (var cardBoard in data)
            {
                await cardBoard.SetupCards();
            }
        }
    }
}

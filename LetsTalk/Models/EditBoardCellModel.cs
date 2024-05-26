using System;

namespace LetsTalk.Models
{
    /// <summary>
    /// This contains optional data that can be populated when navigating from the <see cref="Views.CreateABoardPage"/> or the <see cref="Views.BoardPage"/> to the <see cref="Views.SymbolWorkshopPage"/>.
    /// 
    /// <remarks>
    ///
    ///  Also used by the <see cref="Views.CreateABoardPage"/> to pass the data to the <see cref="Views.SymbolWorkshopPage"/>.
    /// </remarks>
    /// </summary>
    public sealed class EditBoardCellModel
    {
        public CardFileModel Card { get; set; }
        public int Row { get; set; }
        public int Rows { get; set; }

        // If I was using a later version of C#, I would replace all the set methods with init.
        public int Column { get; set;  }
        public int Index { get; set; }
        public int Columns { get; set; }
        public Type PageType { get; set; }
        public CardBoardModel CurrentBoard { get; set; }

    }
}

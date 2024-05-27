
using System.Collections.Generic;
using System.Threading.Tasks;
using LetsTalk.Helpers;
using LetsTalk.Models;

namespace LetsTalk.Database
{
    public sealed class CardModelDatabase : DatabaseBase<CardFileModel>
    {
    

        public override string DatabasePath => $"{DatabaseFolderName}\\";


    }
}

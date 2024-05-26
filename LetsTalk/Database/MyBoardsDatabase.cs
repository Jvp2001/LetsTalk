namespace LetsTalk.Database
{
    public sealed class MyBoardsDatabase : CustomCardBoardDatabase
    {
        public override string DatabasePath => $@"{DatabaseFolderPath}\MyBoards";
    }
}

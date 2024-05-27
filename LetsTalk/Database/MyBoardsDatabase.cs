namespace LetsTalk.Database
{
    public sealed class MyBoardsDatabase : CardBoardDatabase
    {
        public override string DatabasePath => $@"{DatabaseFolderPath}\MyBoards";
    }
}

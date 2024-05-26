namespace LetsTalk.Models
{
    public sealed class BoardPageModel
    {

        public string Name
        {
            get =>  Board.Name;
            set => Board.Name = value;
        }
        public CardBoardModel Board { get; set; }
        public UserType Type { get; set; }




        public BoardPageModel(CardBoardModel board, UserType type)
        {   
            board = board;
            Type = type;
        }

        public static implicit operator CardBoardModel(BoardPageModel boardPageModel)
        {
            return boardPageModel.Board;
        }
    }
}

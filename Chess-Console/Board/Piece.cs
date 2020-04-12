using System;
namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Movements { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            Movements = 0;
        }
        public void MovementIncrease() //method to increase how many movements the piece has made.
        {
            Movements++;
        }
        public abstract bool[,] PossibleMovements();
    }
}

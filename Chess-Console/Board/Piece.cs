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
        public void MovementDecrease()
        {
            Movements--;
        }
        public bool HasPossibleMovements()
        {
            bool[,] possibleMovements = PossibleMovements();
            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (possibleMovements[i, j]) //checks if there is a possible movement, if there is, returns true.
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool CanMoveTo(Position pos)
        {
            bool[,] mat;
            mat = PossibleMovements();
            return mat[pos.Line, pos.Column];
        }
        public abstract bool[,] PossibleMovements();
    }
}   

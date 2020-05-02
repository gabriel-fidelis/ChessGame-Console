using board;
using chess;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {

        }
        public override string ToString()
        {
            return "P";
        }
        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null;
        }
        private bool HasEnemy(Position pos)
        {
            return Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color;
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos;
            //Movements for white
            if (Color == Color.White)
            {
                pos = new Position(Position.Line - 1, Position.Column);
                if (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line - 2, Position.Column); //if it is the first movement of a pawn, it can move two squares foward.
                if (Board.IsValidPosition(pos) && CanMove(pos) && Movements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line - 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) &&  HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line - 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }
            //Movements for black
            else
            {
                pos = new Position(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line + 2, Position.Column); //if it is the first movement of a pawn, it can move two squares foward.
                if (Board.IsValidPosition(pos) && CanMove(pos) && Movements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line + 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line + 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }
            return mat;
        }
    }
}

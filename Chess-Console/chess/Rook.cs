using System;
using board;
namespace chess
{
    class Rook : Piece
    {
        public Rook(Color color, Board board) : base(color, board)
        {

        }
        public override string ToString()
        {
            return "R";
        }
        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != this.Color; //returns true if either the square is empty or there's an enemy piece.
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos;
            //above
            pos = new Position(Position.Line - 1, Position.Column);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line -= 1;
            }
            //below
            pos = new Position(Position.Line + 1, Position.Column);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Line += 1;
            }
            //left 
            pos = new Position(Position.Line, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column -= 1;
            }
            //right
            pos = new Position(Position.Line, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color)
                {
                    break;
                }
                pos.Column += 1;
            }
            return mat;
        }
    }
}


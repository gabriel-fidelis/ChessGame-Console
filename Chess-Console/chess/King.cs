using System;
using board;
namespace chess
{
    class King : Piece
    {
        private ChessMatch chessMatch;
        public King(Color color, Board board, ChessMatch chessMatch) : base(color, board)
        {
            this.chessMatch = chessMatch;
        }
        public override string ToString()
        {
            return "K";
        }
        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != this.Color; //returns true if either the square is empty or there's an enemy piece.
        }
        private bool TestRookToCastle(Position pos) //Test a rook for castling.
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p is Rook && p.Color == this.Color && p.Movements == 0;  
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos;
            //above
            pos = new Position(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //northeast
            pos = new Position(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //right
            pos = new Position(Position.Line, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //southeast
            pos = new Position(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //below
            pos = new Position(Position.Line + 1, Position.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            //southwest
            pos = new Position(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // left
            pos = new Position(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // northwest
            pos = new Position(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            return mat;
        }
    }
}

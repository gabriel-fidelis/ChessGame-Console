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
            //Special-move Castling

            // Basic Castling 
            if (Movements == 0 && !chessMatch.Check) //King cannot use Castling to get out of check, so the match must not be in check.
            {
                Position RookPosition = new Position(this.Position.Line, this.Position.Column + 3);
                if (TestRookToCastle(RookPosition)) //checks if this piece is a rook and everything is ok for it to do a castling
                {
                    Position pos1 = new Position(Position.Line, Position.Column + 1); //first square after king
                    Position pos2 = new Position(Position.Line, Position.Column + 2); //second square after king
                    if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null) //if both squares are empty
                    {
                        mat[Position.Line, Position.Column + 2] = true; //This castling can happen.
                    }

                }
            }
            // Three-Squares Castling
            if (Movements == 0 && !chessMatch.Check)
            {
                Position RookPosition = new Position(this.Position.Line, this.Position.Column - 4);
                if (TestRookToCastle(RookPosition)) //checks if this piece is a rook and everything is ok for it to do a castling
                {
                    Position pos1 = new Position(Position.Line, Position.Column - 1); //third square before king
                    Position pos2 = new Position(Position.Line, Position.Column - 2); //second square before king
                    Position pos3 = new Position(Position.Line, Position.Column - 3); //first square before king
                    if (Board.GetPiece(pos1) == null && Board.GetPiece(pos2) == null && Board.GetPiece(pos3) == null) //if all squares are empty
                    {
                        mat[Position.Line, Position.Column - 2] = true; //This castling can happen.
                    }

                }
            }
            return mat;
        }
    }
}

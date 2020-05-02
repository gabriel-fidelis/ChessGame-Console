using System;
using board;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Color color, Board board) : base(color, board)
        {

        }
        public override string ToString()
        {
            return "Q";
        }
        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != this.Color;
        }
        private bool HasEnemy(Position pos)
        {
            return Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color;
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
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Line--;
            }
            //below
            pos = new Position(Position.Line + 1, Position.Column);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Line++;
            }
            //left 
            pos = new Position(Position.Line, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Column--;
            }
            //right
            pos = new Position(Position.Line, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Column++;
            }
            //northwest
            pos = new Position(Position.Line - 1, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Line--;
                pos.Column--;
            }
            //northeast
            pos = new Position(Position.Line - 1, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Line--;
                pos.Column++;
            }
            //southwest
            pos = new Position(Position.Line + 1, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Line++;
                pos.Column--;
            }
            //southeast
            pos = new Position(Position.Line + 1, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (HasEnemy(pos))
                {
                    break;
                }
                pos.Line++;
                pos.Column++;
            }
            return mat;
        }
    }
}

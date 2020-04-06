using System;
using board.exceptions;
namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;
        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            pieces = new Piece[lines, columns];
        }
        public Piece GetPiece(int line, int column)
        {
            return pieces[line, column];
        }
        public Piece GetPiece(Position pos)
        {
            return pieces[pos.Line, pos.Column];
        }
        private bool ExistsPiece(Position pos)
        {
            CheckPosition(pos);
            return GetPiece(pos) != null;
        }
        public void PutPiece(Piece p, Position pos)
        {
            if (ExistsPiece(pos))
            {
                throw new BoardException("There is already a piece in this position.");
            }
            pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
        private bool IsValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line > Lines || pos.Column < 0 || pos.Column > Columns)
            {
                return false;
            }
            return true;
        }
        public void CheckPosition(Position pos)
        {
            if (!IsValidPosition(pos))
            {
                throw new BoardException("Invalid position.");
            }
        }
    }
}

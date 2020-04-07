using System;
using board.exceptions;
namespace board
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] pieces;
        public Board(int lines, int columns) //Generic board
        {
            Lines = lines;
            Columns = columns;
            pieces = new Piece[lines, columns];
        }
        public Piece GetPiece(Position pos) //Returns an object 'Piece' from the board matrix.
        {
            return pieces[pos.Line, pos.Column];
        }
        private bool ExistsPiece(Position pos) //Control method to check if there's a piece in given position.
        {
            CheckPosition(pos); 
            return GetPiece(pos) != null; //returns true if there's a piece.
        }
        public void PutPiece(Piece p, Position pos) //Put an instance of a Piece in the board matrix of pieces.
        {
            if (ExistsPiece(pos))
            {
                throw new BoardException("There is already a piece in this position.");
            }
            pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
        public Piece RemovePiece(Position pos) //Method that removes a piece in a given position and returns it.
        {
            if (GetPiece(pos) == null)
            {
                return null;
            }
            else
            {
                Piece aux = GetPiece(pos);
                aux.Position = null;
                pieces[pos.Line, pos.Column] = null;
                return aux;
            }

        }
        private bool IsValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line > Lines || pos.Column < 0 || pos.Column > Columns)
            {
                return false;
            }
            return true;
        }
        private void CheckPosition(Position pos)
        {
            if (!IsValidPosition(pos))
            {
                throw new BoardException("Invalid position.");
            }
        }
    }
}

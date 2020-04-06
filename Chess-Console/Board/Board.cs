using System;
using System.Collections.Generic;
using System.Text;

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
        public void PutPiece(Piece p, Position pos)
        {
            pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }
    }
}

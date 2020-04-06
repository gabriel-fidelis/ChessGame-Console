using System;
using board;
using chess;
namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.PutPiece(new Rook(Color.Black, board), new Position(0, 0));
            board.PutPiece(new Rook(Color.Black, board), new Position(1, 3));
            board.PutPiece(new King(Color.Black, board), new Position(2, 4));
            Screen.PrintBoard(board);
        }
    }
}

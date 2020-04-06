using System;
using board;
using chess;
using board.exceptions;
namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);
                board.PutPiece(new Rook(Color.Black, board), new Position(0, 0));
                board.PutPiece(new Rook(Color.Black, board), new Position(0, 0));
                board.PutPiece(new King(Color.Black, board), new Position(2, 4));
                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

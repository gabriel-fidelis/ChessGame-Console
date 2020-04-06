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
                board.PutPiece(new Rook(Color.Yellow, board), new ChessPosition('a', 8).ToPosition());
                board.PutPiece(new King(Color.Yellow, board), new ChessPosition('a', 5).ToPosition());
                Screen.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

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
                ChessMatch match = new ChessMatch();
                while (match.isOver != true)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);
                    Console.Write("\nEnter starting position: ");
                    Position initial = Screen.ReadChessPosition().ToPosition();
                    Console.Clear();
                    bool[,] possibleMovements = match.Board.GetPiece(initial).PossibleMovements();
                    Screen.PrintBoard(match.Board, possibleMovements);
                    Console.Write("\nEnter target position: ");
                    Position final = Screen.ReadChessPosition().ToPosition();
                    match.MovementExecution(initial, final);
                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

using System;
using board;
using chess;
using board.exceptions;
using chess.exceptions;
namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch match = new ChessMatch();
            while (match.isOver != true)
            {
                try
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);
                    Console.WriteLine();
                    Console.WriteLine("Turn: " + match.turn);
                    Console.WriteLine("Waiting movements from " + match.currentPlayer);
                    Console.Write("\nEnter starting position: ");
                    Position initial = Screen.ReadChessPosition().ToPosition();
                    match.CheckStartingPosition(initial);
                    Console.Clear();
                    bool[,] possibleMovements = match.Board.GetPiece(initial).PossibleMovements();
                    Screen.PrintBoard(match.Board, possibleMovements);
                    Console.Write("\nEnter target position: ");
                    Position final = Screen.ReadChessPosition().ToPosition();
                    match.CheckFinalPosition(initial, final);
                    match.TurnExecution(initial, final);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (ChessException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}

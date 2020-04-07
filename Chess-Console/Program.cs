using System;
using board;
using chess;
using board.exceptions;
using System.Collections.Generic;
namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();
                match.MovementExecution(new ChessPosition('c', 1).ToPosition(), new ChessPosition('c', 4).ToPosition());
                Screen.PrintBoard(match.Board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

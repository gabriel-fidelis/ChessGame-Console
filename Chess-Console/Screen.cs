using System;
using System.Collections.Generic;
using chess;
using board;
namespace Chess_Console
{
    static class Screen
    {
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(new Position(i, j)));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }
        public static void PrintBoard(Board board, bool[,] possibleMovements) //method overload to show possible movements of a piece on-screen
        {
            ConsoleColor originalColor = Console.BackgroundColor;
            ConsoleColor PossibleMovementsColor = ConsoleColor.DarkGray;
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMovements[i, j])
                    {
                        Console.BackgroundColor = PossibleMovementsColor;
                    }
                    else
                    {
                        Console.BackgroundColor = originalColor;
                    }
                    PrintPiece(board.GetPiece(new Position(i, j)));
                }
                Console.BackgroundColor = originalColor;
                Console.WriteLine();
            }
            Console.BackgroundColor = originalColor; //just to make sure the background returns to normal.
            Console.WriteLine("  A B C D E F G H");
        }
        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    ConsoleColor originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(piece);
                    Console.ForegroundColor = originalColor;
                }
                else
                {
                    ConsoleColor originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(piece);
                    Console.ForegroundColor = originalColor;
                }
                Console.Write(" ");
            }
        }
        public static ChessPosition ReadChessPosition()
        {
            string position = Console.ReadLine();
            char column = position[0];
            int line = int.Parse(position[1].ToString());
            return new ChessPosition(column, line);
        }
        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            PrintSet(match.GetCapturedPieces(Color.White));
            Console.Write("Black: ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            PrintSet(match.GetCapturedPieces(Color.Black    ));
        }
        private static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece piece in set)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
            Console.ResetColor();
            Console.WriteLine();
        }
        public static void PrintMatch(ChessMatch match) 
        {
            Console.WriteLine("Turn: " + match.Turn);
            Console.WriteLine();
            PrintCapturedPieces(match);
            if (match.IsOver)
            {
                Console.WriteLine("Check-Mate!");
                Color winnerColor = match.CurrentPlayer;
                if (winnerColor == Color.White)
                {
                    Console.Write("Winner: ");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("White!");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("Winner: ");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("White!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.WriteLine("Waiting movements from " + match.CurrentPlayer);
                if (match.Check)
                {
                    Console.WriteLine("Attention, your king is in check!");
                }
                Console.Write("\nEnter starting position: ");
                Position initial = ReadChessPosition().ToPosition();
                match.CheckStartingPosition(initial);
                Console.Clear();
                bool[,] possibleMovements = match.Board.GetPiece(initial).PossibleMovements();
                PrintBoard(match.Board, possibleMovements); //Printing board with possible movements of a currently moving piece.
                Console.Write("\nEnter target position: ");
                Position final = ReadChessPosition().ToPosition();
                match.CheckFinalPosition(initial, final);
                match.TurnExecution(initial, final);
            }

        }
    }
}

using System;
using chess;
using board;
namespace Chess_Console
{
    class Screen
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
            ConsoleColor OriginalColor = Console.BackgroundColor;
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
                        Console.BackgroundColor = OriginalColor;
                    }
                    PrintPiece(board.GetPiece(new Position(i, j)));
                }
                Console.WriteLine();
                Console.BackgroundColor = OriginalColor;
            }
            Console.BackgroundColor = OriginalColor;
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
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
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
    }
}

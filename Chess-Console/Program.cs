/* Chess Match Boardgame WIP - Made in .NET Core
 * This project is a proof of my knowledge in object orientation and also a good start in the creation of my portfolio.
 * I've put a lot of work in it and it shouldn't be too hard to read because I've commented the code in the important methods.
 * I'll give an brief explanation of the code here too.
 * -- Board Folder --
 * This folder contains classes not related to chess, but a boardgame in general, the class Board for example has methods to put and remove pieces
 * and control the exceptions this may cause (putting pieces off a board limit for example.)
 * -- Chess Folder --
 * This folder contains all classes related to chess, such as its pieces and their movement actions and the rules of a Chess Match (contained in the ChessMatch class)
 * The class ChessPosition takes a chess position-like command and converts it to a matrix position of a board.
 */

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
            while (!match.IsOver)
            {
                try
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);
                    Console.WriteLine();
                    Screen.PrintMatch(match);
                }
                catch (BoardException e) //catches exceptions related to a Board
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (ChessException e) //catches exceptions related to chess' rules
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect Position.");
                    Console.ReadLine();
                }
            }
            Console.Clear();
            Screen.PrintBoard(match.Board);
            Screen.PrintMatch(match);
        }
    }
}

using System;
using System.Collections.Generic;
using board;
using chess.exceptions;
using board.exceptions;
namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool IsOver { get; private set; }
        public bool Check { get; private set; }
        private HashSet<Piece> playingPieces;
        private HashSet<Piece> capturedPieces;
        public ChessMatch()
        {
            Board = new board.Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            playingPieces = new HashSet<Piece>();
            capturedPieces = new HashSet<Piece>();
            PutPieces();
            IsOver = false;
            Check = false;
        }
        public HashSet<Piece> GetCapturedPieces(Color color) //get color-specific captured pieces.
        {
            HashSet<Piece> SubSet = new HashSet<Piece>();
            foreach (Piece piece in capturedPieces) //creates a subset with only the color-specific captured pieces;
            {
                if (piece.Color == color)
                {
                    SubSet.Add(piece);
                }
            }
            return SubSet;
        }
        public HashSet<Piece> GetPlayingPieces(Color color) //get color-specific currently playing pieces.
        {
            HashSet<Piece> SubSet = new HashSet<Piece>();
            foreach (Piece piece in playingPieces) //creates a subset with only the color-specific playing pieces;
            {
                if (piece.Color == color)
                {
                    SubSet.Add(piece);
                }
            }
            return SubSet;
        }
        private Color GetOpponentColor(Color color) 
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        private Piece GetKing(Color color) //Get a color-specific king in currently playing pieces.
        {
            foreach (Piece piece in GetPlayingPieces(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }
        public bool KingIsInCheck(Color color) //Check if opponent's king is in check.
        {
            Piece King = GetKing(color);
            foreach (Piece piece in GetPlayingPieces(GetOpponentColor(color)))
            {
                bool[,] pieceMovements = piece.PossibleMovements();
                if (pieceMovements[King.Position.Line, King.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        /*tests every possible movement from a player's currently playing pieces, 
        *if there's not a movement that would get their king out of check, the game is over.
        */
        public bool CheckMateTest(Color color)
        {
            foreach (Piece piece in GetPlayingPieces(color))
            {
                bool[,] possibleMovements = piece.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMovements[i, j])
                        {
                            Position initial = piece.Position;
                            Position final = new Position(i, j);
                            Piece capturedPiece = MovementExecution(initial, final);
                            bool CheckTest = KingIsInCheck(color);
                            UndoMovement(initial, final, capturedPiece);
                            if (!CheckTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public Piece MovementExecution(Position initial, Position final)
        {
            Piece p = Board.RemovePiece(initial);
            p.MovementIncrease();
            Piece capturedPiece = Board.RemovePiece(final); //This line captures a piece if there is one in the final movement position of another piece.
            Board.PutPiece(p, final);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece); // if a piece was captured during the movement, add it to the set
                playingPieces.Remove(capturedPiece); // and remove it from the playing pieces.
            }
            return capturedPiece;
        }
        private void UndoMovement(Position initial, Position final, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(final);
            p.MovementDecrease();
            if (capturedPiece != null)
            {
                Board.PutPiece(capturedPiece, final);
                capturedPieces.Remove(capturedPiece);
                playingPieces.Add(capturedPiece);
            }
            Board.PutPiece(p, initial);
        }
        public void TurnExecution(Position initial, Position final)
        {
            Piece capturedPiece = MovementExecution(initial, final);
            if (KingIsInCheck(CurrentPlayer))
            {
                UndoMovement(initial, final, capturedPiece);
                throw new ChessException("You cannot make a move that will result in you being in check.");
            }
            Piece possiblePawn = Board.GetPiece(final); //Checks if the piece moving is a pawn for possible promotion.
            if (possiblePawn is Pawn)
            {
                if (((possiblePawn.Color == Color.White && final.Line == 0) || (possiblePawn.Color == Color.Black && final.Line == 7)))
                {
                    playingPieces.Add(PromotePawn(possiblePawn, initial, final, capturedPiece)); //promotes pawn in the final position
                }
            }
            if (KingIsInCheck(GetOpponentColor(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (CheckMateTest(GetOpponentColor(CurrentPlayer)))
            {
                IsOver = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
        }
        private Piece PromotePawn(Piece p, Position initial, Position final, Piece capturedPiece) //Pawn Promotion method
        {
            p = Board.RemovePiece(final);
            playingPieces.Remove(p);
            Console.WriteLine();
            Console.WriteLine("Your pawn has been promoted! choose your piece (Bishop/Queen/Knight/Rook)");
            Console.Write("Desired piece: ");
            string desiredPiece = Console.ReadLine().ToLower();
            if (desiredPiece == "bishop")
            {
                Bishop bishop = new Bishop(CurrentPlayer, Board);
                Board.PutPiece(bishop, final);
                return bishop;
            }
            else if (desiredPiece == "queen")
            {
                Queen queen = new Queen(CurrentPlayer, Board);
                Board.PutPiece(queen, final);
                return queen;
            }
            else if (desiredPiece == "knight")
            {
                Knight knight = new Knight(CurrentPlayer, Board);
                Board.PutPiece(knight, final);
                playingPieces.Add(Board.GetPiece(final));
                return knight;
            }
            else if (desiredPiece == "rook")
            {
                Rook rook = new Rook(CurrentPlayer, Board);
                Board.PutPiece(rook, final);
                playingPieces.Add(Board.GetPiece(final));
                return rook;
            }
            else
            {
                Board.PutPiece(p, initial);
                if (capturedPiece != null)
                {
                    Board.PutPiece(capturedPiece, final);
                    capturedPieces.Remove(capturedPiece);
                }
                throw new ChessException("Chosen piece is incorrect. (You need to choose between Bishop, Queen, Rook or Knight.");
            }
        }
        private void ChangePlayer() //Method to change current player
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }
        public void CheckStartingPosition(Position pos)
        {
            if (Board.GetPiece(pos) == null)
            {
                throw new BoardException("There isn't a piece in this position.");
            }
            if (CurrentPlayer != Board.GetPiece(pos).Color)
            {
                throw new ChessException("It is " + CurrentPlayer.ToString() + "'s turn!");
            }
            if (!Board.GetPiece(pos).HasPossibleMovements())
            {
                throw new ChessException("There isn't possible movements for this piece.");
            }
        }
        public void CheckFinalPosition(Position initial, Position final)
        {
            Board.CheckPosition(final);
            if (!Board.GetPiece(initial).CanMoveTo(final))
            {
                throw new ChessException("Piece cannot move to this position.");
            }
        }
        private void PutNewPiece(char column, int line, Piece piece)
        {
            Board.PutPiece(piece, new ChessPosition(column, line).ToPosition());
            playingPieces.Add(piece);
        }
        private void PutPieces()
        {
            //white
            PutNewPiece('a', 1, new Rook(Color.White, Board));
            PutNewPiece('h', 1, new Rook(Color.White, Board));
            PutNewPiece('e', 1, new King(Color.White, Board));
            PutNewPiece('c', 1, new Bishop(Color.White, Board));
            PutNewPiece('f', 1, new Bishop(Color.White, Board));
            PutNewPiece('d', 1, new Queen(Color.White, Board));
            PutNewPiece('b', 1, new Knight(Color.White, Board));
            PutNewPiece('g', 1, new Knight(Color.White, Board));
            int code = 97; //ascii code of 'a', to create pawns.
            for (int i = 0; i < Board.Lines; i++)
            {
                char pawns = (char) code;
                PutNewPiece(pawns, 2, new Pawn(Color.White, Board));
                code++;
            }
            //black
            PutNewPiece('a', 8, new Rook(Color.Black, Board));
            PutNewPiece('h', 8, new Rook(Color.Black, Board));
            PutNewPiece('e', 8, new King(Color.Black, Board));
            PutNewPiece('c', 8, new Bishop(Color.Black, Board));
            PutNewPiece('f', 8, new Bishop(Color.Black, Board));
            PutNewPiece('d', 8, new Queen(Color.Black, Board));
            PutNewPiece('b', 8, new Knight(Color.Black, Board));
            PutNewPiece('g', 8, new Knight(Color.Black, Board));
            code = 97;
            for (int i = 0; i < Board.Lines; i++)
            {
                char pawns = (char)code;
                PutNewPiece(pawns, 7, new Pawn(Color.Black, Board));
                code++;
            }
        }
    }
}

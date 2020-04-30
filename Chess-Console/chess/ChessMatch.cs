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
            if (!KingIsInCheck(color))
            {
                return false;
            }
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
            PutNewPiece('c', 1, new Rook(Color.White, Board));
            PutNewPiece('d', 1, new King(Color.White, Board));
            PutNewPiece('h', 7, new Rook(Color.White, Board));
            //black
            PutNewPiece('a', 8, new King(Color.Black, Board));
            PutNewPiece('b', 8, new Rook(Color.Black, Board));
        }
    }
}

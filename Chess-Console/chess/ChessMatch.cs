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
        public void MovementExecution(Position initial, Position final)
        {
            Piece p = Board.RemovePiece(initial);
            p.MovementIncrease();
            Piece capturedPiece = Board.RemovePiece(final); //This line captures a piece if there is one in the final movement position of another piece.
            Board.PutPiece(p, final);
            if (capturedPiece != null)
            {
                capturedPieces.Add(capturedPiece); // if a piece was captured during the movement, add it to the set.
            }
        }
        public void TurnExecution(Position initial, Position final) 
        {
            MovementExecution(initial, final);
            Turn++;
            ChangePlayer(); 
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
            PutNewPiece('c', 2, new Rook(Color.White, Board));
            PutNewPiece('d', 2, new Rook(Color.White, Board));
            PutNewPiece('e', 2, new Rook(Color.White, Board));
            PutNewPiece('e', 1, new Rook(Color.White, Board));
            PutNewPiece('d', 1, new King(Color.White, Board));
            //black
            PutNewPiece('c', 7, new Rook(Color.Black, Board));
            PutNewPiece('c', 8, new Rook(Color.Black, Board));
            PutNewPiece('d', 7, new Rook(Color.Black, Board));
            PutNewPiece('e', 7, new Rook(Color.Black, Board));
            PutNewPiece('e', 8, new Rook(Color.Black, Board));
            PutNewPiece('d', 8, new King(Color.Black, Board));

        }
    }
}

using System;
using board;
using chess.exceptions;
using board.exceptions;
namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool isOver { get; private set; }
        public ChessMatch()
        {
            Board = new board.Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            PutPieces();
            isOver = false;
        }
        public void MovementExecution(Position initial, Position final)
        {
            Piece p = Board.RemovePiece(initial);
            p.MovementIncrease();
            Piece capturedPiece = Board.RemovePiece(final); //This line captures a piece if there is one in the final movement position of another piece.
            Board.PutPiece(p, final);
        }
        public void TurnExecution(Position initial, Position final) 
        {
            MovementExecution(initial, final);
            turn++;
            ChangePlayer();
        }
        private void ChangePlayer() //Method to change current player
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }
        public void CheckStartingPosition(Position pos)
        {
            if (Board.GetPiece(pos) == null)
            {
                throw new BoardException("There isn't a piece in this position.");
            }
            if (currentPlayer != Board.GetPiece(pos).Color)
            {
                throw new ChessException("It is " + currentPlayer.ToString() + "'s turn!");
            }
            if (!Board.GetPiece(pos).HasPossibleMovements())
            {
                throw new ChessException("There isn't possible movements for this piece.");
            }
        }
        public void CheckFinalPosition(Position initial, Position final)
        {
            if (!Board.GetPiece(initial).CanMoveTo(final))
            {
                throw new ChessException("Piece cannot move to this position.");
            }
        }
        private void PutPieces() 
        {
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.PutPiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.PutPiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }
    }
}

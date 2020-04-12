using System;
using board;
namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int turn;
        private Color currentPlayer;
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
        private void PutPieces()
        {
            Board.PutPiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.PutPiece(new King(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.PutPiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.PutPiece(new King(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
        }
    }
}

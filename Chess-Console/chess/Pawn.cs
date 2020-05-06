using board;
using chess;

namespace chess
{
    class Pawn : Piece
    {
        public bool EnPassantVulnerable { get; set; }
        private ChessMatch match; 
        public Pawn(Color color, Board board, ChessMatch match) : base(color, board)
        {
            EnPassantVulnerable = false;
            this.match = match;
        }
        public override string ToString()
        {
            return "P";
        }
        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null;
        }
        private bool HasEnemy(Position pos)
        {
            return Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != this.Color;
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos;
            //Movements for white
            if (Color == Color.White)
            {
                pos = new Position(Position.Line - 1, Position.Column);
                if (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line - 2, Position.Column); //if it is the first movement of a pawn, it can move two squares foward.
                if (Board.IsValidPosition(pos) && CanMove(pos) && Movements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line - 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) &&  HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line - 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                //Special-move En-Passant 

                pos = new Position(Position.Line, Position.Column - 1); //Left
                if (match.enPassantPawn != null)
                {
                    if (Board.IsValidPosition(pos) && match.enPassantPawn.Color != this.Color && match.enPassantPawn.Position.Line == pos.Line && match.enPassantPawn.Position.Column == pos.Column)
                    {
                        if (Board.GetPiece(new Position(pos.Line - 1, pos.Column)) == null)
                        {
                            mat[pos.Line - 1, pos.Column] = true;
                        }
                    }
                }
                pos = new Position(Position.Line, Position.Column + 1); //Right
                if (match.enPassantPawn != null)
                {
                    if (Board.IsValidPosition(pos) && match.enPassantPawn.Color != this.Color && match.enPassantPawn.Position.Line == pos.Line && match.enPassantPawn.Position.Column == pos.Column)
                    {
                        if (Board.GetPiece(new Position(pos.Line - 1, pos.Column)) == null)
                        {
                            mat[pos.Line - 1, pos.Column] = true;
                        }
                    }
                }
            }
            //Movements for black
            else
            {
                pos = new Position(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line + 2, Position.Column); //if it is the first movement of a pawn, it can move two squares foward.
                if (Board.IsValidPosition(pos) && CanMove(pos) && Movements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line + 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos = new Position(Position.Line + 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && HasEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                //Special-move En-Passant
                pos = new Position(Position.Line, Position.Column - 1); //Left
                if (match.enPassantPawn != null)
                {
                    if (Board.IsValidPosition(pos) && match.enPassantPawn.Color != this.Color && match.enPassantPawn.Position.Line == pos.Line && match.enPassantPawn.Position.Column == pos.Column)
                    {
                        if (Board.GetPiece(new Position(pos.Line + 1, pos.Column)) == null)
                        {
                            mat[pos.Line + 1, pos.Column] = true;
                        }
                    }
                }
                pos = new Position(Position.Line, Position.Column + 1); //Right
                if (match.enPassantPawn != null)
                {
                    if (Board.IsValidPosition(pos) && match.enPassantPawn.Color != this.Color && match.enPassantPawn.Position.Line == pos.Line && match.enPassantPawn.Position.Column == pos.Column)
                    {
                        if (Board.GetPiece(new Position(pos.Line + 1, pos.Column)) == null)
                        {
                            mat[pos.Line + 1, pos.Column] = true;
                        }
                    }
                }
            }
            return mat;
        }
    }
}

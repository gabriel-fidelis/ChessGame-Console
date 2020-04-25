using board;
namespace chess
{
    class ChessPosition //This class controls how the chess positioning system interacts with the board matrix.
    {
        public char Column { get; set; }
        public int Line { get; set; }
        public ChessPosition(char column, int line)
        {
            Column = column;
            Line = line;
        }
        public Position ToPosition() //Method to transform chess position, e.g. "e6", to a matrix position.
        {
            return new Position(8 - Line, Column - 'a');
        }
        public override string ToString() //Override ToString method to print chess position.
        {
            return Column + Line.ToString();
        }
    }
}

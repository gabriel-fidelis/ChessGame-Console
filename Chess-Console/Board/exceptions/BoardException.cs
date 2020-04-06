using System;
namespace board.exceptions
{
    class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message)
        {

        }
    }
}

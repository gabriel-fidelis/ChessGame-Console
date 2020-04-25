using System;
using System.Collections.Generic;
using System.Text;

namespace chess.exceptions
{
    class ChessException : ApplicationException
    {
        public ChessException(string message) : base(message)
        {

        }
    }
}

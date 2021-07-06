using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sudoku.BaseGame.exceptions
{
    class CellNotFoundException : Exception
    {
        public CellNotFoundException()
        {
        }

        public CellNotFoundException(string message) : base(message)
        {
        }

        public CellNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CellNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

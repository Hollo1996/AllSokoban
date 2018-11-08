using Sokoban_2._0_Console_Tester.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Sokoban_2._0_Console_Tester
{
    [Serializable]
    internal class WrongCoordException : TextedException
    {

        public WrongCoordException()
        {
        }

        public WrongCoordException(string message) : base(message)
        {
        }

        public WrongCoordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongCoordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
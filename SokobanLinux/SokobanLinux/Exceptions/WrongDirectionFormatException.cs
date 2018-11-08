using Sokoban_2._0_Console.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Sokoban_2._0_Console.Exceptions
{
    [Serializable]
    internal class WrongLineFormatException :  TextedException
    {
        public WrongLineFormatException()
        {
        }

        public WrongLineFormatException(string message) : base(message)
        {
        }

        public WrongLineFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongLineFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
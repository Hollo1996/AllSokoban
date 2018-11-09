using Sokoban_2._0_Console_Tester.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Sokoban_2._0_Console_Tester
{
    [Serializable]
    internal class WrongDirectionFormat :  TextedException
    {
        public WrongDirectionFormat()
        {
        }

        public WrongDirectionFormat(string message) : base(message)
        {
        }

        public WrongDirectionFormat(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongDirectionFormat(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
﻿using Sokoban_2._0_Console_Tester.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Sokoban_2._0_Console_Tester
{
    [Serializable]
    internal class WrongLineFormat :  TextedException
    {
        public WrongLineFormat()
        {
        }

        public WrongLineFormat(string message) : base(message)
        {
        }

        public WrongLineFormat(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongLineFormat(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
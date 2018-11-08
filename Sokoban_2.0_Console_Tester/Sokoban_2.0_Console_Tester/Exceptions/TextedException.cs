using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester.Exceptions
{
    [Serializable]
    public class TextedException: Exception
    {
        private const string title= "Wrong Coordinate Format Usage:";
        private string Text {
            get => title + Message;
        }

        public TextedException()
        {
        }

        public TextedException(string message) : base(message)
        {
        }

        public TextedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TextedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}

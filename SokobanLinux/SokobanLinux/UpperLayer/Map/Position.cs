using System;
using System.Text.RegularExpressions;
using Sokoban_2._0_Console.Exceptions;

namespace Sokoban_2._0_Console.UpperLayer.Map
{
    public class Position
    {
		int column;
		int line;
        public int Column
		{
			get => column;
			set => column=value;
		}
        public int Line
		{
			get => line;
			set => line=value;
		}
		public int this[int dimension]{
			get{
                if (dimension == 0)
                    return column;
                if (dimension == 1)
                    return line;
				throw new ArgumentOutOfRangeException("Position has only two coordinates!");
			}
            set
            {
				if (dimension == 0)
				{
					column = value;
					return;
				}
				if(dimension==1)
                {
                    line = value;
                    return;
                }
                throw new ArgumentOutOfRangeException("Position has only two coordinates!");
            }
		}
		
        public Position(int _column,int _line)
        {
		    column = _column;
			line = _line;
		}
        public Position()
        {
            column = 0;
            line = 0;
        }
		public Position(String s){
			if (!Regex.IsMatch(s, "^\\(\\d{1,};\\d{1,}\\)$"))
                throw new WrongCoordException(s + " is not in the correct coord format");
			int deviderIndex = s.IndexOf(';');
			column = int.Parse(s.Substring(1,deviderIndex-1));
			line = int.Parse(s.Substring(deviderIndex+1,s.Length-deviderIndex-2));
		}

        public static bool operator ==(Position p1, Position p2)
        {         
			return Position.Equals(p1,p2)||(p1.line == p2.line && p1.column == p2.column);
        }
        public static bool operator !=(Position p1, Position p2)
        {
            return !(p1 == p2);
        }

		/*public static Position operator +(Position p1, Position p2){
			return new Position(p1.column+p2.column, p1.line + p2.line);
		}*/

		public override string ToString()
		{
			return "("+column+";"+line+")";
		}
	}
}

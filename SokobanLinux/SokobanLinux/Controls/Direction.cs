using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban_2._0_Console.UpperLayer.Map;

namespace Sokoban_2._0_Console.Controls
{
    public class Direction
    {
		public enum dir{
            // The 4 Direction  we can choose
            UP, DOWN, LEFT, RIGHT
        }

		public static readonly Direction[] all = new Direction[] {
			new Direction(dir.UP,0,new Position(0,1)),
			new Direction(dir.RIGHT,1,new Position(1,0)),
			new Direction(dir.DOWN,2,new Position(0,-1)),
			new Direction(dir.LEFT,3,new Position(-1,0)) };
		public static Direction UP => all[0];
        public static Direction RIGHT => all[1];
        public static Direction DOWN => all[2];
        public static Direction LEFT => all[3];

		public readonly Position vector;
        public readonly dir value;
		public readonly byte id;
		private Direction(dir _value, byte _id, Position _vector){
			value=_value;
			id = _id;
			vector = _vector;
		}

        // A function that returns the opposite for a certain direction
        public Direction GetReverse()
        {
            return all[(id+2)%4];
        }

		public static Direction GetById(int _id){
			return all[_id];
		}


    }
}

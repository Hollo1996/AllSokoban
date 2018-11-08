using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Controls
{
    public class Direction
    {
        public enum dir {
            // The 4 Direction  we can choose
            UP, DOWN, LEFT, RIGHT
        }
        public static Direction UP => new Direction(dir.UP,dir.DOWN);
        public static Direction DOWN => new Direction(dir.DOWN, dir.UP);
        public static Direction RIGHT => new Direction(dir.RIGHT, dir.LEFT);
        public static Direction LEFT => new Direction(dir.LEFT, dir.RIGHT);

        public readonly dir direction;
        public readonly dir opposite;
        Direction(dir _direction, dir _opposite){ direction=_direction; opposite = _opposite; }

        // A function that returns the opposite for a certain direction
        public Direction GetReverse()
        {
            return new Direction(opposite,direction);
        }

    }
}

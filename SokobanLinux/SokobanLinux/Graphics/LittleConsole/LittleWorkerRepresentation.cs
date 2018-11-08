using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Graphics.Base;
using Sokoban_2._0_Console.Moveables;

namespace Sokoban_2._0_Console.Graphics.LittleConsole
{
    public class LittleWorkerRepresentation : WorkerRepresentation, LittleRepresentation
    {
        Worker owner;
        public Worker Owner { set => owner = value; }

        private char[,] representation = new char[,] { { 'W' } };
        public char[,] Representation
        {
            get
            {
                owner.LoadRepresentation();
				switch (inDirection.value)
                {
                    case Direction.dir.UP:  representation[0,0] = 'W'; break;
                    case Direction.dir.DOWN: representation[0, 0] = 'M'; break;
                    case Direction.dir.LEFT: representation[0, 0] = '3'; break;
                    case Direction.dir.RIGHT: representation[0, 0] = 'E'; break;
                    default:
                        break;
                }
                return representation;
            }
        }

        Direction inDirection = null;
        public Direction InDirection
        {
            set
            {
                inDirection = value;
            }
        }
    }
}

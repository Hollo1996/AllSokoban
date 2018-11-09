using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Moveables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Graphics.Base
{
    public interface WorkerRepresentation:MoveableRepresentation
    {
        Worker Owner { set; }
        Direction InDirection { set; }
    }
}

using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Features;
using Sokoban_2._0_Console.Moveables;
using Sokoban_2._0_Console.UpperLayer.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Graphics.Base
{
    public interface FieldRepresentation
    {
        Field Owner { set; }
        Moveable OnOwner { set; }
        int Friction { set; }
        Feature _Feature { set; }
        Dictionary<Direction.dir, Field> Neighbours{set;}    }
}

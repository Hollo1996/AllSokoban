using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Features;
using Sokoban_2._0_Console.Graphics.Base;
using Sokoban_2._0_Console.Moveables;
using Sokoban_2._0_Console.UpperLayer.Map;

namespace Sokoban_2._0_Console.Graphics.LittleConsole
{
    class LittleFieldRepresentation : FieldRepresentation, LittleRepresentation
    {
        private Field owner=null;
        public Field Owner { set=>owner=value; }
        private Moveable onOwner = null;
        public Moveable OnOwner { set => onOwner = value; }
        private int friction = 0;
        public int Friction { set => friction = value; }
        private Feature feature = null;
        public Feature _Feature { set => feature = value; }
        private Dictionary<Direction.dir,Field> neighbours = null;
        public Dictionary<Direction.dir, Field> Neighbours { set => neighbours = value;  }

        char[,] representation = new char[,] { { '+', '-', '-', '-', '+' }, { '|', ' ', ' ', ' ', '|' }, { '+', '-', '-', '-', '+' } };
        public char[,] Representation
        {
            get
            {
                owner.LoadRepresentation();
                if (onOwner != null)
                    representation[1, 2] = ((LittleRepresentation)(onOwner.Representation)).Representation[0, 0];
                else
                    representation[1, 2] = ' ';

                if (feature != null)
                    representation[1, 1] = ((LittleRepresentation)(feature.Representation)).Representation[0, 0];
                else
                    representation[1, 1] = ' ';

                if (friction == Liquid.Honey.GetHashCode())
                    representation[1, 3] = 'm';
                else if (friction == Liquid.Oil.GetHashCode())
                    representation[1, 3] = '~';
                else
                    representation[1, 3] = ' ';

                if (neighbours.ContainsKey(Direction.dir.UP))
                    if (neighbours[Direction.dir.UP].coordColumn != owner.coordColumn ||
                        neighbours[Direction.dir.UP].coordLine != owner.coordLine - 1)
                        for (int i = 1; i < 4; i++)
                            representation[0, i] = '~';
                    else
                        for (int i = 1; i < 4; i++)
                            representation[0, i] = ' ';

                if (neighbours.ContainsKey(Direction.dir.DOWN))
                    if (neighbours[Direction.dir.DOWN].coordColumn != owner.coordColumn ||
                        neighbours[Direction.dir.DOWN].coordLine != owner.coordLine + 1)
                        for (int i = 1; i < 4; i++)
                            representation[2, i] = '~';
                    else
                        for (int i = 1; i < 4; i++)
                            representation[2, i] = ' ';

                if (neighbours.ContainsKey(Direction.dir.RIGHT))
                    if (neighbours[Direction.dir.RIGHT].coordLine != owner.coordLine ||
                        neighbours[Direction.dir.RIGHT].coordColumn != owner.coordColumn + 1)
                            representation[1, 4] = 'S';
                    else
                        representation[1, 4] = ' ';

                if (neighbours.ContainsKey(Direction.dir.LEFT))
                    if (neighbours[Direction.dir.LEFT].coordLine != owner.coordLine ||
                        neighbours[Direction.dir.LEFT].coordColumn != owner.coordColumn - 1)
                        representation[1, 0] = 'S';
                    else
                        representation[1, 0] = ' ';

                return representation;
            }
        }
    }
}

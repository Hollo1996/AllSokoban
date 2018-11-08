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
		Field.RepresentableData data=null;      
		public Field.RepresentableData Data { set => data = value; }

        private bool start = true;
        private static readonly char[] horizontalOuterWalls = { ' ', '-', '~', '~' };
        private static readonly char[] horizontalInnerWalls = { ' ', '-', 'O', '@' };
        private static readonly char[] verticalWalls = { ' ', '|', 'O', '@' };
        private static readonly char[] liquid = { 'm', '~' };
        private static readonly char corner = '+';
        private static readonly char empty = ' ';
        private char[,] representation = new char[3, 5];
        public char[,] Representation
        {
            get
            {
				lock (data.modifiedSync)
					data.modified = false;
					

				if (data.Functionality != null)
					representation[1, 1] = ((LittleRepresentation)(data.Functionality.Representation)).Representation[0, 0];
                else
                    representation[1, 1] = empty;

				if (data.OnThis != null)
					representation[1, 2] = ((LittleRepresentation)(data.OnThis.Representation)).Representation[0, 0];
                else
                    representation[1, 2] = empty;

				if (data.Friction == Liquid.Honey.GetHashCode())
                    representation[1, 3] = liquid[0];
				else if (data.Friction == Liquid.Oil.GetHashCode())
                    representation[1, 3] = liquid[1];
                else
                    representation[1, 3] = empty;

                if (start)
                {
					Direction d;
					Position selfPosition=data.Self.position;
                    Position neighbourPosition;
                    byte index;

                    for (int i = 0; i < 4; i++)
                        representation[(i / 2) * 2, (((i + 1) % 4) / 2) * 4] = corner;

                    for (byte i = 0; i < 4; i++)
                    {
                        d = Direction.GetById(i);
						if (!data.IsIsolated && !Equals(data.Self[d.value],null))
                        {
							neighbourPosition = data.Self[d.value];
							if (neighbourPosition.Column != selfPosition.Column + d.vector.Column ||
							    neighbourPosition.Line != selfPosition.Line - d.vector.Line)
							{
								if (!Equals(data.Container[neighbourPosition][d.GetReverse().value],null) &&
								    data.Container[neighbourPosition][d.GetReverse().value] == selfPosition)
									index = 2;
								else
									index = 3;
							}                                
                            else
                                index = 0;
                        }
                        else
                        {
                            index = 1;
                        }



                        if (d.id % 2 == 0)
                        {
                            representation[1 - d.vector.Line, 1] = horizontalOuterWalls[index];
                            representation[1 - d.vector.Line, 2] = horizontalInnerWalls[index];
                            representation[1 - d.vector.Line, 3] = horizontalOuterWalls[index];
                        }
                        else
                        {
                            representation[1, 2 + (d.vector.Column * 2)] = verticalWalls[index];
                        }
                    }
                    start = false;
                }
                return representation;
            }
        }
	}
}

using Sokoban_2._0_Console;
using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Exceptions;
using Sokoban_2._0_Console.Graphics;
using Sokoban_2._0_Console.Moveables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.UpperLayer.Map
{
    public class GameField
    {
        private static FieldContainer container;
        public static FieldContainer FieldContainer
        {
            set => container = value;
        }
        private Dictionary<String, int> scores = new Dictionary<String, int>();
        private ControlInterface[] relavantControls;
        //protected List<Box> boxes = new List<Box>();
        private List<int[]> spawnPlaces = new List<int[]>();
        private Position coordinates;
        private Position min;
        private Position max;
        private bool start;
        // private Dictionary<int, Dictionary<int, Box>> boxesMap = new Dictionary<int, Dictionary<int, Box>>();
        // private Dictionary<int, Dictionary<int, BoxContainer>> boxContainerMap = new Dictionary<int, Dictionary<int, BoxContainer>>();
        // private Dictionary<int, Dictionary<int, FallTrap>> fallTrapMap = new Dictionary<int, Dictionary<int, FallTrap>>();
        // private Dictionary<int, Dictionary<int, Button>> buttonMap = new Dictionary<int, Dictionary<int, Button>>();
        //private Dictionary<int, Dictionary<int, Hole>> holeMap = new Dictionary<int, Dictionary<int, Hole>>();
        string[] paramList;
        string state;

        private GameField() { }
        private static GameField onlyInstance = new GameField();
        public static GameField Instance => onlyInstance;

        public void LoadMap(string filename)
        {
            // Read the file and display it line by line.  
            using (StreamReader file = new StreamReader(filename + @".txt"))
            {
                container.fieldsMap.Clear();
                spawnPlaces.Clear();
                scores.Clear();
                relavantControls = null;
                state = "none";
                start = true;
                max = min = null;

                string inputLine;
                string[] datas;
                List<char> tmp;
                while ((inputLine = file.ReadLine()) != null)
                {
                    tmp = inputLine.ToList();
                    tmp.RemoveAll(x => x == ' ' || x == '\t' || x == '\n');
                    inputLine = new String(tmp.ToArray()).ToUpper();
                    if (inputLine.StartsWith("#", StringComparison.Ordinal) || string.Compare(inputLine, "", StringComparison.Ordinal) == 0)
                        continue;

                    if (inputLine.Contains(":"))
                    {
                        state = inputLine.Substring(0, inputLine.IndexOf(':') + 1);
                        if (inputLine.EndsWith(":", StringComparison.Ordinal))
                            continue;
                        inputLine = inputLine.Substring(inputLine.IndexOf(':') + 1, inputLine.Length - inputLine.IndexOf(':') - 1);
                    }

                    datas = inputLine.Split(',');
                    for (int i = 0; i < datas.Length; i++)
                        dataProcessor(datas[i]);
                    /*foreach (int xc in boxesMap.Keys)
                    {
                        foreach (int yc in boxesMap[xc].Keys)
                        {
                            boxes.Add(boxesMap[xc][yc]);
                        }
                    }*/
                }
            }
            Field.FinishInit();
            Graphic.Instance.pictureSize = new Position((max.Column - min.Column + 1) * 5, (max.Line - min.Line + 1) * 3);
        }

        private void dataProcessor(string data)
        {
            switch (state)
            {
                case "FIELD:":
                    initField(data);
                    break;
                case "BOX:":
                    initBox(data);
                    break;
                case "BOXCONATAINER:":
                    initBoxContainer(data);
                    break;
                case "BUTTON:":
                    initButton(data);
                    break;
                case "FALLTRAP:":
                    initFallTrap(data);
                    break;
                case "HOLE:":
                    initHole(data);
                    break;
                case "NEIGHBOURHOOD:":
                    initNeighbourhood(data);
                    break;
                case "NEIGHBORHOOD:":
                    initNeighbourhood(data);
                    break;
                case "WIRE:":
                    initWire(data);
                    break;
                case "COMPATIBILITY:":
                    initCompatibility(data);
                    break;
                case "SPAWN:":
                    initSpawn(data);
                    break;
                default:
                    throw new WrongLineFormat("Wrong Status header before line:" + data);
            }
        }

        private void initField(string data)
		{
            bool directional = data.Contains("<D>");
			bool connected = data.Contains("<>")||directional;
			bool spectrum = data.Contains("--")||connected;
            Position startCoordinates;
            Position endCoordinates;
            int swap;
            if (spectrum)
            {
				if(connected){
                    startCoordinates = new Position(data.Substring(0, data.IndexOf('<')));
                    endCoordinates = new Position(data.Substring(data.IndexOf('>') + 1, data.Length - data.IndexOf('>') - 1));
				}
				else{
                    startCoordinates = new Position(data.Substring(0, data.IndexOf('-')));
                    endCoordinates = new Position(data.Substring(data.IndexOf('-') + 2, data.Length - data.IndexOf('-') - 2));
                }
                if (startCoordinates.Column > endCoordinates.Line)
                {
                    swap = startCoordinates.Column;
                    startCoordinates.Column = endCoordinates.Column;
                    endCoordinates.Column = swap;
                }
                if (startCoordinates.Line > endCoordinates.Line)
                {
                    swap = startCoordinates.Line;
                    startCoordinates.Line = endCoordinates.Line;
                    endCoordinates.Line = swap;
                }
            }
            else
            {
                startCoordinates = new Position(data);
                endCoordinates = new Position(data);
            }

            for (int column = startCoordinates.Column; column <= endCoordinates.Column; column++)
            {
                for (int line = startCoordinates.Line; line <= endCoordinates.Line; line++)
                {
                    coordinates = new Position(column, line);
                    if (!container.fieldsMap.ContainsKey(coordinates.Column))
                    {
                        container.fieldsMap.Add(coordinates.Column, new Dictionary<int, Field>());
                    }

                    Field f = new Field(coordinates, Game.viewFactory.Field);
                    container.fieldsMap[coordinates.Column].Add(coordinates.Line, f);
                    
                    if(connected && column != startCoordinates.Column){
                        f.AddNeighbour(new Position(column - 1, line), Direction.LEFT,false);
                        container.fieldsMap[column-1][line].AddNeighbour(f.position, Direction.RIGHT, false);
                    }
                    if (connected && line != startCoordinates.Line)
                    {
                        f.AddNeighbour(new Position(column, line-1), Direction.UP, false);
                        container.fieldsMap[column][line-1].AddNeighbour(f.position, Direction.DOWN, false);
                    }
                    if (directional && column == endCoordinates.Column)
                    {
                        f.AddNeighbour(new Position(startCoordinates.Column,line), Direction.RIGHT, false);
                        container.fieldsMap[startCoordinates.Column][line].AddNeighbour(f.position, Direction.LEFT, false);
                    }
                    if (directional && line == endCoordinates.Line)
                    {
                        f.AddNeighbour(new Position(column, startCoordinates.Line), Direction.DOWN, false);
                        container.fieldsMap[column][startCoordinates.Line].AddNeighbour(f.position, Direction.UP, false);
                    }              

                }
            }

            if (start)
            {
                min = new Position(startCoordinates.Column, startCoordinates.Line);
                max = new Position(endCoordinates.Column, endCoordinates.Line);
                start = false;
            }
            else
            {
                if (startCoordinates.Column < min.Column)
                    min.Column = startCoordinates.Column;
                if (startCoordinates.Line < min.Line)
                    min.Line = startCoordinates.Line;
                if (endCoordinates.Column > max.Column)
                    max.Column = endCoordinates.Column;
                if (endCoordinates.Line > max.Line)
                    max.Line = endCoordinates.Line;
            }
        }

        private void initBox(string line)
        {
            /*coordinates = new Position(line);
            if (!(container.fieldsMap.ContainsKey(coordinates.Column) && container.fieldsMap[coordinates.Column].ContainsKey(coordinates.Line)))
            {
                throw new WrongCoordException("NOPE");
            }

            if (!boxesMap.ContainsKey(coordinates[x]))
            {
                boxesMap.Add(coordinates[x], new Dictionary<int, Box>());
            }

            boxesMap[coordinates[x]].Add(coordinates[y], new Box());
            container.fieldsMap[coordinates[x]][coordinates[y]].AddMoveable(boxesMap[coordinates[x]][coordinates[y]]);*/
        }

        private void initSpawn(string line)
        {

            paramList = line.Split('_');
            coordinates = new Position(paramList[0]);
            if (!(container.fieldsMap.ContainsKey(coordinates.Column) && container.fieldsMap[coordinates.Column].ContainsKey(coordinates.Line)))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + line);
            }

            int[] dtmp = { coordinates.Column, coordinates.Line, Int32.Parse(paramList[1]) };
            spawnPlaces.Add(dtmp);
        }

        private void initBoxContainer(string line)
        {
            /*
            coordinates = new Position(inputLine);
            if (!(fieldsMap.ContainsKey(coordinates[x]) && container.fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("NOPE");
            }

            if (!boxContainerMap.ContainsKey(coordinates[x]))
            {
                boxContainerMap.Add(coordinates[x], new Dictionary<int, BoxContainer>());
            }

            boxContainerMap[coordinates[x]].Add(coordinates[y], new BoxContainer());
            container.fieldsMap[coordinates[x]][coordinates[y]].AddFeature(boxContainerMap[coordinates[x]][coordinates[y]]);*/

        }

        private void initHole(string line)
        {
            /*
            coordinates = new Position(inputLine);
            if (!(fieldsMap.ContainsKey(coordinates[x]) && container.fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
            }

            if (!holeMap.ContainsKey(coordinates[x]))
            {
                holeMap.Add(coordinates[x], new Dictionary<int, Hole>());
            }

            holeMap[coordinates[x]].Add(coordinates[y], new Hole());
            container.fieldsMap[coordinates[x]][coordinates[y]].AddFeature(holeMap[coordinates[x]][coordinates[y]]);*/

        }

        private void initButton(string line)
        {
            /*
            coordinates = new Position(inputLine);
            if (!(fieldsMap.ContainsKey(coordinates[x]) && container.fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("NOPE");
            }

            if (!buttonMap.ContainsKey(coordinates[x]))
            {
                buttonMap.Add(coordinates[x], new Dictionary<int, Button>());
            }

            buttonMap[coordinates[x]].Add(coordinates[y], new Button());
            container.fieldsMap[coordinates[x]][coordinates[y]].AddFeature(buttonMap[coordinates[x]][coordinates[y]]);*/
        }

        private void initFallTrap(string line)
        {
            /*
            coordinates = new Position(inputLine);
            if (!(fieldsMap.ContainsKey(coordinates[x]) && container.fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
            }

            if (!fallTrapMap.ContainsKey(coordinates[x]))
            {
                fallTrapMap.Add(coordinates[x], new Dictionary<int, FallTrap>());
            }

            fallTrapMap[coordinates[x]].Add(coordinates[y], new FallTrap(fieldsMap[coordinates[x]][coordinates[y]]));
            container.fieldsMap[coordinates[x]][coordinates[y]].AddFeature(fallTrapMap[coordinates[x]][coordinates[y]]);*/
        }

		private struct Flags
		{
			public bool towardsRight;
            public bool towardsLeft;
			public bool givenRight;
            public bool givenLeft;
			public bool[] directions;
			public bool random;
			public bool immediate;
			public bool optional;
			public bool add;
            public Flags(bool _random,
                         bool _towardsRightSide, bool _towardsLeftSide,
                         bool _givenRightSide, bool _givenLeftSide,
                         bool _directionsToUp, bool _directionsToRight, bool _directionsToDown, bool _directionsToLeft,
                         bool _immediate, bool _optional, bool _add)
            {
                random = _random;
                towardsRight = _towardsRightSide;
                towardsLeft = _towardsLeftSide;
                givenRight = _givenRightSide;
                givenLeft = _givenLeftSide;
                directions = new bool[] { _directionsToUp, _directionsToRight, _directionsToDown, _directionsToLeft };
                immediate = _immediate;
                optional = _optional;
                add = _add;
            }
			public void Set(bool _random,
                         bool _towardsRightSide, bool _towardsLeftSide,
                         bool _givenRightSide, bool _givenLeftSide,
                         bool _directionsToUp, bool _directionsToRight, bool _directionsToDown, bool _directionsToLeft,
                         bool _immediate, bool _optional, bool _add){
				random = _random;
                towardsRight = _towardsRightSide;
                towardsLeft = _towardsLeftSide;
                givenRight = _givenRightSide;
                givenRight = _givenLeftSide;
                directions[0] = _directionsToUp;
				directions[1] = _directionsToRight;
				directions[2] = _directionsToDown;
				directions[3] = _directionsToLeft;
                immediate = _immediate;
                optional = _optional;
                add = _add;
            }
			public void Set(string flagPart)
			{
                Random r = new Random();
				int tmp;
				if(flagPart[0]=='-' && flagPart[flagPart.Length-1] == '-'){
					tmp=r.Next(0 , 3);
					switch(tmp){
						case 0:
							towardsRight = true;
							towardsLeft = false;
							break;
						case 1:
							towardsRight = false;
							towardsLeft = true;
							break;
						case 2:
							towardsRight = true;
							towardsLeft = true;
                            break;
					}
				}
				else
				{
					if (flagPart[0] == '<')
						towardsLeft = true;
					if(flagPart[flagPart.Length - 1] == '-')
						towardsRight = true;
					
				}

				for (int i = 1; i < flagPart.Length-2; i++)
                {
                    switch (flagPart[i])
                    {
						case 'U':
							if(flagPart[i+1]=='P')
							{
                                directions[0] = true;
								i++;
							}
							break;
						case 'R':
                            if (flagPart[i + 1] == 'I')
                            {
                                directions[1] = true;
                                i++;
							}
                            else if (flagPart[i + 1] == 'A')
                            {
								random = true;
                                i++;
                            }
							break;
						case 'D': 
                            if (flagPart[i + 1] == 'O')
                            {
                                directions[2] = true;
                                i++;
                            }
							break;
						case 'L': 
                            if (flagPart[i + 1] == 'E')
                            {
                                directions[3] = true;
                                i++;
                            }
							break;
						case 'O':
                            if (flagPart[i + 1] == 'R')
							{
                                directions[0] = true;
                                directions[2] = true;
                                i++;
                            }
                            break;
						case 'H':
                            if (flagPart[i + 1] == 'O')
                            {
                                directions[0] = true;
                                directions[2] = true;
                                i++;
                            }
                            break;
						case 'A':
                            if (flagPart[i + 1] == 'L')
							{
                                directions[0] = true;
                                directions[1] = true;
                                directions[2] = true;
                                directions[3] = true;
                                i++;
                            }
                            else if (flagPart[i + 1] == 'U')
							{
                                directions[0] = false;
                                directions[1] = false;
                                directions[2] = false;
                                directions[3] = false;
                                i++;
                            }
                            break;

                        case '#': immediate = true; break;
                        case '=': immediate = false; break;

                        case '?': optional = true; break;
                        case '!': optional = false; break;

                        case '+': add = true; break;
                        case '×': add = false; break;
                        default: break;
                    }
                }
            }
		}


        private List<Direction> direction = new List<Direction>();
		private List<Position> left=new List<Position>();
		private List<Position> right=new List<Position>();
		private Position known = null;
		private Position toCalculate = null;
		private Flags flags = new Flags(false, false, false, true, true, false, false, false, false, true, true, true);
        private void initNeighbourhood(string data)
		{
			//Clear data;
			direction.Clear();
			left.Clear();
			right.Clear();
			//the indexes of the first end last character of a coord
            int coordStart;
			int coordEnd = data.IndexOf(')');
            //parts of the incoming data
            String rightCoordPart;
            String typeFlagsPart;
			string leftCoordPart = data.Substring(0, coordEnd + 1);
			string remainingPart = data.Substring(coordEnd + 1, data.Length - coordEnd-1);
            //is the left coordinate given
			if (string.Compare(leftCoordPart, "(;)", StringComparison.Ordinal) == 0)
			{
				flags.givenLeft = false;
			}
			else
			{
				flags.givenLeft = true;
				left.Add(new Position(leftCoordPart));
			}
            //Processing all of the right sides connecting to the left side.
			while (remainingPart != "")
			{
                //Clear data;
                direction.Clear();
                right.Clear();
				//calculate the start and the end of the next coord
				coordEnd = remainingPart.IndexOf(')');
				coordStart = remainingPart.IndexOf('(');
				//calculate the next coordinate in the remaining string and the flags about it's connection tod the left coordinate.
				rightCoordPart = remainingPart.Substring(coordStart, coordEnd - coordStart + 1);
				typeFlagsPart = remainingPart.Substring(0, coordStart);
				//cutting down the used data
				remainingPart = remainingPart.Substring(coordEnd + 1, remainingPart.Length - coordEnd - 1);
                
                //regenerate flags from string
				flags.Set(false, false, false, true, true, false, false, false, false, true, true, true);
				flags.Set(typeFlagsPart);
                //is the right coordinate given
                if (string.Compare(leftCoordPart, "(;)", StringComparison.Ordinal) == 0)
                {
                    flags.givenRight = false;
                }
                else
                {
                    flags.givenRight = true;
					right.Add(new Position(rightCoordPart));
                }
				//random irány generálása, ha szükséges
                Random r;
				int tmp;
				if(flags.random){
					r = new Random();
                    tmp = r.Next(0, 16);
					for (int i = 0; i<4 ; i++)
					{
						flags.directions[i] = (i % 2 == 1);
						tmp /= 2;
					}
				}
				//Check for semantic error
				if((!flags.givenLeft && (!flags.directions[0] && !flags.directions[1] && !flags.directions[2] && !flags.directions[3]))||
				    (!flags.givenRight && (!flags.directions[0] && !flags.directions[1] && !flags.directions[2] && !flags.directions[3])) ||
				    (!flags.givenLeft && !flags.givenRight))
					throw new WrongLineFormatException("Too many data missing at line"+data);
				//Calculate random directions
				if((!flags.directions[0] && !flags.directions[1] && !flags.directions[2] && !flags.directions[3]))
				{
					flags.directions[0] = (left[0].Line > right[0].Line);
					flags.directions[2] = (left[0].Line < right[0].Line);
					flags.directions[1] = (left[0].Column < right[0].Column);
					flags.directions[3] = (left[0].Column > right[0].Column);
				}

                for (int i = 0; i < 4; i++)
				{
					if(flags.directions[i])
                        direction.Add(Direction.GetById(i));
				}

				if (!flags.givenLeft || !flags.givenRight)
				{
					if (flags.givenRight)
						known = right[0];
					else
						known = left[0];
                    if (!Equals(known, null) && !(container.fieldsMap.ContainsKey(known.Column) && container.fieldsMap[known.Column].ContainsKey(known.Line)))
						throw new WrongCoordException("Wrong Coords Added at line: " + data);

                    int[][] Keys = new int[2][];
                    Keys[0] = container.fieldsMap[known.Column].Keys.ToArray();
                    Keys[1] = container.fieldsMap.Keys.ToArray();
                    int otherEnd = -1;
                    int previous = -1;
					
					for (int i = 0; i < 4; i++)
                    {
                        if (!flags.directions[i])
                            continue;
						toCalculate = new Position();
                        otherEnd = previous = Keys[i % 2].Min() * (((i + 3) % 4) / 2) + Keys[i % 2].Max() * (((i + 1) % 4) / 2);
						foreach (int coord in Keys[i % 2])
                        {
                            if (i % 2 == 0 || container.fieldsMap[coord].ContainsKey(known.Line))
                            {
                                if ((coord > otherEnd && i % 3 == 0) ||
                                    (coord < otherEnd && i % 3 != 0))
                                    otherEnd = coord;

                                if ((coord > previous && coord < known[(i + 1) % 2] && i % 3 == 0) ||
                                    (coord < previous && coord > known[(i + 1) % 2] && i % 3 != 0))
                                    previous = coord;
                            }
                        }

                        if (!flags.immediate)
                        {
                            toCalculate[i % 2] = known[i % 2];

                            if (previous != known[(i + 1) % 2])
                                toCalculate[(i + 1) % 2] = previous;
                            else
                                toCalculate[(i + 1) % 2] = otherEnd;
                        }

						if (!flags.givenRight)
							right.Add(toCalculate);
						else
                            left.Add(toCalculate);
                    }

					             
                }
				int index = 0;
                foreach (Position pr in right)
                {
                    foreach (Position pl in left)
					{
                        if (flags.add)
                        {
                            if (flags.towardsRight)
                                container[pl].AddNeighbour(pr, direction[index], !flags.optional);
                            else
                                container[pr].AddNeighbour(pl, direction[index].GetReverse(), !flags.optional);
                        }
                        else
                        {
                            if (flags.towardsLeft)
                                container[pl].RemoveNeighbour(direction[index]);
                            else
                                container[pr].RemoveNeighbour(direction[index].GetReverse());
                        }
                        index++;
                    }
                }
			}
        }

        

        private void initWire(string line)
        {
            /*
            coordinateParts = inputLine.Split('-');

            coordinates = new Position(coordinateParts[0]); // parse first part
            if (!(buttonMap.ContainsKey(coordinates[x]) && buttonMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + inputLine + ". In state:" + state);
            }

            Button buttontmp = buttonMap[coordinates[x]][coordinates[y]];

            coordinates = new Position(coordinateParts[1]); // parse second part
            if (!(fallTrapMap.ContainsKey(coordinates[x]) && fallTrapMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + inputLine + ". In state:" + state);
            }

            FallTrap fallTraptmp = fallTrapMap[coordinates[x]][coordinates[y]];

            buttontmp.AddSwitchable(fallTraptmp);*/
        }

        private void initCompatibility(string line)
        {
            /*coordinateParts = inputLine.Split('-');
            coordinates = new Position(coordinateParts[0]); // parse first part
            if (!(fieldsMap.ContainsKey(coordinates[x]) && container.container.fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
            }

            if (!(boxContainerMap.ContainsKey(coordinates[x]) && boxContainerMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("NOPE");
            }

            BoxContainer boxContainertmp = boxContainerMap[coordinates[x]][coordinates[y]];

            coordinates = new Position(coordinateParts[1]); // parse second part

            if (!(fieldsMap.ContainsKey(coordinates[x]) && container.fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
            }

            if (!(boxesMap.ContainsKey(coordinates[x]) && boxesMap[coordinates[x]].ContainsKey(coordinates[y])))
            {
                throw new WrongCoordException("Wrong Coords Added at line: " + inputLine + ". In state:" + state);
            }

            Box boxtmp = boxesMap[coordinates[x]][coordinates[y]];

            boxContainertmp.AddBox(boxtmp);*/
        }

        private static Direction stringToDirection(string s)
        {
            switch (s)
            {
                case "L": return Direction.LEFT;
                case "R": return Direction.RIGHT;
                case "U": return Direction.UP;
                case "D": return Direction.DOWN;
                default: throw new WrongLineFormatException(s + " stands foreach no direction");

            }
        }

        // ReMoves the given box from the boxes list
        /*public void RemoveBox(Box box)
        {
            boxes.Remove(box);
        }*/

        // Incrementing the score of the player
        public void Score(String name)
        {
            if (scores.ContainsKey(name))
                scores[name]++;
            else
                scores.Add(name, 1);
        }

        // If any control interface is out of worker, this function Gets called
        // If at least one control interface has a worker, returns
        // else, calls the game field's endGame() function
        public void OutOfWorkers()
        {
            foreach (ControlInterface co in relavantControls)
            {
                if (co.hasWorker())
                    return;
            }
            Game.Instance.Stop();
        }


        // Sets control interfaces
        public void AddControlInterface(ControlInterface[] controlInterfaces)
        {
            if (controlInterfaces.Length > spawnPlaces.Count)
                return;
            relavantControls = controlInterfaces;
            Worker[] workers = new Worker[controlInterfaces.Length];
            for (int i = 0; i < spawnPlaces.Count() && i < controlInterfaces.Length; i++)
            {
                workers[i] = new Worker(spawnPlaces[i][2], Game.viewFactory.Worker);
                controlInterfaces[i].AddWorker(workers[i]);
                workers[i].SetOwner(controlInterfaces[i]);
                container.fieldsMap[spawnPlaces[i][0]][spawnPlaces[i][1]].AddMoveable(workers[i]);
            }
        }

        // Returns the players ranking by iterating through the hash map
        // and incrementing the ranking if someone has a higher score
        public int GetPlace(String name)
        {
            int place = 1;
            foreach (int other_score in scores.Values)
            {
                if (other_score > scores[name])
                {
                    place++;
                }
            }

            return place;
        }

        /*public void setBoxes(List<Box> boxes)
        {
            this.boxes = boxes;
        }*/
    }
}

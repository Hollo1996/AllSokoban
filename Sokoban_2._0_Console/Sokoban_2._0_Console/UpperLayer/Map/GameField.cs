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
        private Dictionary<String, int> scores = new Dictionary<String, int>();
        private ControlInterface[] relavantControls;
        //protected List<Box> boxes = new List<Box>();
        Dictionary<int, Dictionary<int, Field>> fieldsMap = new Dictionary<int, Dictionary<int, Field>>();
        protected List<int[]> spawnPlaces = new List<int[]>();
        protected int spawnedCount = 0;
        const short line = 0;
        const short column = 1;

        private GameField() { }
        private static GameField onlyInstance = new GameField();
        public static GameField Instance=>onlyInstance;

        public void LoadMap(string filename)
        {
            // Read the file and display it line by line.  
            using (StreamReader file = new StreamReader(filename + @".txt"))
            {
                fieldsMap.Clear();
                spawnPlaces.Clear();
                scores.Clear();
                relavantControls = null;
                spawnedCount = 0;

                /*Dictionary<int, Dictionary<int, Box>> boxesMap = new Dictionary<int, Dictionary<int, Box>>();
                Dictionary<int, Dictionary<int, BoxContainer>> boxContainerMap = new Dictionary<int, Dictionary<int, BoxContainer>>();
                Dictionary<int, Dictionary<int, FallTrap>> fallTrapMap = new Dictionary<int, Dictionary<int, FallTrap>>();
                Dictionary<int, Dictionary<int, Button>> buttonMap = new Dictionary<int, Dictionary<int, Button>>();
                Dictionary<int, Dictionary<int, Hole>> holeMap = new Dictionary<int, Dictionary<int, Hole>>();*/
                int[] coordinates;
                int minLine=-1;
                int maxLine=-1;
                int minColumn = -1;
                int maxColumn = -1;

                string inputLine;
                string state = "none";
                string[] coordinateParts;
                string[] paramList;
                while ((inputLine = file.ReadLine()) != null)
                {
                    if (inputLine.EndsWith(":"))
                    { // inputLine is the next state
                        state = inputLine;
                    }
                    else
                    {
                        switch (state)
                        {
                            case "fields:":
                                coordinates = CoordSplit(inputLine);
                                if (!fieldsMap.ContainsKey(coordinates[line]))
                                {
                                    fieldsMap.Add(coordinates[line], new Dictionary<int, Field>());
                                }

                                Field f = new Field(coordinates[line], coordinates[column],Game.viewFactory.Field);
                                fieldsMap[coordinates[line]].Add(coordinates[column], f);
                                if (minLine == -1)
                                {
                                    minLine = coordinates[line];
                                    maxLine = coordinates[line];
                                    minColumn = coordinates[column];
                                    maxColumn = coordinates[column];
                                }
                                else
                                {
                                    if (coordinates[line] < minLine)
                                        minLine = coordinates[line];
                                    if (coordinates[line] > maxLine)
                                        maxLine = coordinates[line];
                                    if (coordinates[column] < minColumn)
                                        minColumn = coordinates[column];
                                    if (coordinates[column] > maxColumn)
                                        maxColumn = coordinates[column];
                                }
                                break;
                            /*case "box:":
                                coordinates = CoordSplit(inputLine);
                                if (!(fieldsMap.ContainsKey(coordinates[x]) && fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("NOPE");
                                }

                                if (!boxesMap.ContainsKey(coordinates[x]))
                                {
                                    boxesMap.Add(coordinates[x], new Dictionary<int, Box>());
                                }

                                boxesMap[coordinates[x]].Add(coordinates[y], new Box());
                                fieldsMap[coordinates[x]][coordinates[y]].AddMoveable(boxesMap[coordinates[x]][coordinates[y]]);
                                break;*/
                            /*case "boxcontainers:":
                                coordinates = CoordSplit(inputLine);
                                if (!(fieldsMap.ContainsKey(coordinates[x]) && fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("NOPE");
                                }

                                if (!boxContainerMap.ContainsKey(coordinates[x]))
                                {
                                    boxContainerMap.Add(coordinates[x], new Dictionary<int, BoxContainer>());
                                }

                                boxContainerMap[coordinates[x]].Add(coordinates[y], new BoxContainer());
                                fieldsMap[coordinates[x]][coordinates[y]].AddFeature(boxContainerMap[coordinates[x]][coordinates[y]]);
                                break;*/
                            /*case "button:":
                                coordinates = CoordSplit(inputLine);
                                if (!(fieldsMap.ContainsKey(coordinates[x]) && fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("NOPE");
                                }

                                if (!buttonMap.ContainsKey(coordinates[x]))
                                {
                                    buttonMap.Add(coordinates[x], new Dictionary<int, Button>());
                                }

                                buttonMap[coordinates[x]].Add(coordinates[y], new Button());
                                fieldsMap[coordinates[x]][coordinates[y]].AddFeature(buttonMap[coordinates[x]][coordinates[y]]);
                                break;*/
                            /*case "falltrap:":
                                coordinates = CoordSplit(inputLine);
                                if (!(fieldsMap.ContainsKey(coordinates[x]) && fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
                                }

                                if (!fallTrapMap.ContainsKey(coordinates[x]))
                                {
                                    fallTrapMap.Add(coordinates[x], new Dictionary<int, FallTrap>());
                                }

                                fallTrapMap[coordinates[x]].Add(coordinates[y], new FallTrap(fieldsMap[coordinates[x]][coordinates[y]]));
                                fieldsMap[coordinates[x]][coordinates[y]].AddFeature(fallTrapMap[coordinates[x]][coordinates[y]]);
                                break;*/
                            /*case "hole:":
                                coordinates = CoordSplit(inputLine);
                                if (!(fieldsMap.ContainsKey(coordinates[x]) && fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
                                }

                                if (!holeMap.ContainsKey(coordinates[x]))
                                {
                                    holeMap.Add(coordinates[x], new Dictionary<int, Hole>());
                                }

                                holeMap[coordinates[x]].Add(coordinates[y], new Hole());
                                fieldsMap[coordinates[x]][coordinates[y]].AddFeature(holeMap[coordinates[x]][coordinates[y]]);
                                break;*/
                            case "neighbourhood:":
                                paramList = inputLine.Split(' ');
                                coordinateParts = paramList[0].Split('-');
                                coordinates = CoordSplit(coordinateParts[0]); // parse first part
                                if (!(fieldsMap.ContainsKey(coordinates[line]) && fieldsMap[coordinates[line]].ContainsKey(coordinates[column])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
                                }

                                Field first = fieldsMap[coordinates[line]][coordinates[column]];

                                coordinates = CoordSplit(coordinateParts[1]); // parse first part


                                if (!(fieldsMap.ContainsKey(coordinates[line]) && fieldsMap[coordinates[line]].ContainsKey(coordinates[column])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
                                }

                                Field second = fieldsMap[coordinates[line]][coordinates[column]];

                                first.AddNeighbour(second, stringToDirection(paramList[2]));
                                second.AddNeighbour(first, stringToDirection(paramList[1]));
                                break;
                            /*case "wires:":
                                coordinateParts = inputLine.Split('-');

                                coordinates = CoordSplit(coordinateParts[0]); // parse first part
                                if (!(buttonMap.ContainsKey(coordinates[x]) && buttonMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine + ". In state:" + state);
                                }

                                Button buttontmp = buttonMap[coordinates[x]][coordinates[y]];

                                coordinates = CoordSplit(coordinateParts[1]); // parse second part
                                if (!(fallTrapMap.ContainsKey(coordinates[x]) && fallTrapMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine + ". In state:" + state);
                                }

                                FallTrap fallTraptmp = fallTrapMap[coordinates[x]][coordinates[y]];

                                buttontmp.AddSwitchable(fallTraptmp);
                                break;*/
                            /*case "compatibility:":
                                coordinateParts = inputLine.Split('-');
                                coordinates = CoordSplit(coordinateParts[0]); // parse first part
                                if (!(fieldsMap.ContainsKey(coordinates[x]) && fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
                                }

                                if (!(boxContainerMap.ContainsKey(coordinates[x]) && boxContainerMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("NOPE");
                                }

                                BoxContainer boxContainertmp = boxContainerMap[coordinates[x]][coordinates[y]];

                                coordinates = CoordSplit(coordinateParts[1]); // parse second part

                                if (!(fieldsMap.ContainsKey(coordinates[x]) && fieldsMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
                                }

                                if (!(boxesMap.ContainsKey(coordinates[x]) && boxesMap[coordinates[x]].ContainsKey(coordinates[y])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine + ". In state:" + state);
                                }

                                Box boxtmp = boxesMap[coordinates[x]][coordinates[y]];

                                boxContainertmp.AddBox(boxtmp);
                                break;*/
                            case "spawnplaces:":
                                paramList = inputLine.Split(' ');
                                coordinates = CoordSplit(paramList[0]);
                                if (!(fieldsMap.ContainsKey(coordinates[line]) && fieldsMap[coordinates[line]].ContainsKey(coordinates[column])))
                                {
                                    throw new WrongCoordException("Wrong Coords Added at line: " + inputLine);
                                }

                                int[] dtmp = {coordinates[line],coordinates[column], Int32.Parse(paramList[1]) };
                                spawnPlaces.Add(dtmp);
                                break;
                            default:
                                //throw new WrongLineFormat("Wrong Status header before line:" + inputLine);
                                break;
                        }
                    }
                }
                Graphic.Instance.SetPictureSize((maxLine - minLine+1)*3, (maxColumn - minColumn+1)*5);
                Graphic.Instance.SetMap(fieldsMap);

                /*foreach (int xc in boxesMap.Keys)
                {
                    foreach (int yc in boxesMap[xc].Keys)
                    {
                        boxes.Add(boxesMap[xc][yc]);
                    }
                }*/
            }
        }


        private static int[] CoordSplit(string s)
        {

            if (!s.StartsWith("(") || !s.EndsWith(")") || s.Split(';').Length != 2)
            {
                throw new WrongCoordException(s+" is not in the correct coord format");
            }

            s=s.Substring(1, s.Length - 2);

            string[] coordinates = s.Split(';');

            int[] coordinatesParsed = new int[2];
            coordinatesParsed[line] = int.Parse(coordinates[line]);
            coordinatesParsed[column] = int.Parse(coordinates[column]);

            return coordinatesParsed;
        }

        private static Direction stringToDirection(string s)
        {
            switch (s)
            {
                case "L": return Direction.LEFT;
                case "R": return Direction.RIGHT;
                case "U": return Direction.UP;
                case "D": return Direction.DOWN;
                default: throw new WrongDirectionFormat(s + " stands foreach no direction");

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
                workers[i] = new Worker(spawnPlaces[i][2],Game.viewFactory.Worker);
                controlInterfaces[i].AddWorker(workers[i]);
                workers[i].SetOwner(controlInterfaces[i]);
                fieldsMap[spawnPlaces[i][0]][spawnPlaces[i][1]].AddMoveable(workers[i]);
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

        //Called on keyboard inAdd
        /*public void onEvent(Event e)
        {
            controlInterfaces.ForEach(x=>x.OnEvent(e));
        }*/

        public void setFields(Dictionary<int, Dictionary<int, Field>> _fieldsMap)
        {
            fieldsMap = _fieldsMap;
        }

        /*public void setControlInterfaces(List<ControlInterface> controlInterfaces)
        {
            this.controlInterfaces = controlInterfaces;
        }*/

        /*public void setBoxes(List<Box> boxes)
        {
            this.boxes = boxes;
        }*/

        /*public void listWorkers()
        {
            foreach (ControlInterface ci in controlInterfaces)
            {
                int score = 0;
                if (scores.ContainsKey(ci.id))
                {
                    score = scores[ci.id];
                }
                ci.listWorker(score);
            }
        }*/

        public void listFeatures(string s)
        {
            foreach (Dictionary<int, Field> fm in fieldsMap.Values)
            {
                foreach (Field f in fm.Values)
                {
                    f.PrintFeature(s);
                }
            }
        }

        public void listFields()
        {
            foreach (Dictionary<int, Field> fm in fieldsMap.Values)
            {
                foreach (Field f in fm.Values)
                {
                    f.Print();
                }
            }
        }

       /* public void listBoxes()
        {
            foreach (Box b in boxes)
            {
                b.Print();
            }
        }*/

        public void listFieldFieldConnectivities()
        {
            int max_row = 20;
            int max_col = 20;

            for (int i = 0; i < max_col; i++)
            {
                for (int k = 0; k < max_row; k++)
                {
                    foreach (Dictionary<int, Field> fm in fieldsMap.Values)
                    {
                        foreach (Field f in fm.Values)
                        {
                            if (f.coordLine == i && f.coordColumn == k)
                            {
                                f.listFieldConnectives();
                            }
                        }
                    }
                }
            }
        }

        public void listButtonConnectives()
        {
            foreach (Dictionary<int, Field> fm in fieldsMap.Values)
            {
                foreach (Field f in fm.Values)
                {
                    f.PrintFeature("buttonConnectives");
                }
            }
        }

        /*public void drawFields(SokobanView jf)
        {
            foreach (Field f in fields)
            {
                f.draw(jf);
            }
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    public class Field : MoveableVisitor
    {
        private Dictionary<Direction, Field> neighbours = new Dictionary<Direction, Field>();
        private Moveable onThis;
        private Feature feature;
        private Direction enter;
        private bool isIsolated;
        public readonly int coordX;
        public readonly int coordY;
        private double friction;
        private double remainedStrength;

        // Constructor for the fields
        public Field(int x, int y)
        {
            coordX = x;
            coordY = y;
            isIsolated = false;
        }

        // Adds the given field to its neighbours, in the given direction
        public void AddNeighbour(Field field, Direction direction)
        {
            neighbours.Add(direction, field);
        }

        // Adds a feature to the field
        public void AddFeature(Feature feature)
        {
            this.feature = feature;
        }

        // Adds a Moveable object to the field
        public void AddMoveable(Moveable Moveable)
        {
            onThis = Moveable;
            Moveable.SetField(this);
            if (feature != null)
                feature.Interact(Moveable);
        }

        // The worker calls for the LeaveRequest function when it is Moved by the player
        // If the field has a neighbor in the given direction and that field is not Isolated,
        // the worker Gets Pushed
        public void LeaveRequest(Direction direction, Worker w)
        {
            if (neighbours.ContainsKey(direction) && !neighbours[direction].GetIsolated())
            {
                neighbours[direction].Push(direction, w, w.strength);
            }
        }

        // A box is being Pushed to this field
        // If this field has a Moveable object on it, the object on this field Gets Pushed to the next field, in the same direction
        // If not, the object given as a parameter will be set onto this field and Gets reMoved from its previous field
        public void Push(Direction direction, Box box, double remStrength)
        {
            if (onThis != null)
            {
                if (neighbours.ContainsKey(direction))
                    onThis.Pushed(box, neighbours[direction]);
                else
                    onThis.Pushed(box, null);


                if (onThis != null && ((remStrength - friction) > 0))
                {
                    enter = direction;
                    remainedStrength = remStrength - friction;
                    onThis.Accept(this);
                }
            }
            if (onThis == null)
            {
                onThis = box;
                box.SetField(this);
                neighbours[GetReverse()].RemoveMoveable();

                if (feature != null)
                {
                    feature.Interact(box);
                }
            }
        }

        private Direction GetReverse()
        {
            throw new NotImplementedException();
        }

        // A worker is being Pushed to this field
        // If this field has a Moveable object on it, the worker Gets destroyed
        // After the object given as a parameter will be set onto this field and Gets reMoved from its previous field
        public void Push(Direction direction, Worker worker, double remStrength)
        {
            if (onThis != null)
            {
                onThis.Pushed(worker, null);

                if (onThis != null && ((remStrength - friction) > 0))
                {
                    enter = direction;
                    remainedStrength = remStrength - friction;
                    onThis.Accept(this);
                }
            }

            if (onThis == null)
            {
                onThis = worker;
                worker.SetField(this);
                neighbours[direction.GetReverse()].RemoveMoveable();

                if (feature != null)
                {
                    feature.Interact(worker);
                }
            }
        }

        // ReMoves the Moveable object that is located on the field
        public void RemoveMoveable()
        {
            onThis = null;
        }

        // Destroys the Moveable object that is located on this field
        public void DestroyMoveable()
        {
            if (onThis != null)
            {
                onThis.Destroy();
            }
        }

        // Isolating the field from its neighbours by changing the isIsolated attribute
        public void Isolate()
        {
            isIsolated = true;
        }

        // Returns the value stored in the isIsolated attribute
        public bool GetIsolated()
        {
            return isIsolated;
        }

        // Returns whether the field is empty or not
        public bool IsEmpty()
        {
            return onThis == null;
        }

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a box
        // If this field has a neighbor in the previously stored (enter) direction and that neighbor is not Isolated,
        // the box Gets Pushed to the next field
        public void Visit(Box b)
        {
            if (neighbours.ContainsKey(enter) && !neighbours[enter].GetIsolated())
            {
                neighbours[enter].Push(enter, b, remainedStrength);
            }
        }

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a worker
        // If this field has a neighbor in the previously stored (enter) direction and that neighbor is not Isolated and it's empty
        // the worker Gets Pushed to the next field
        public void Visit(Worker w)
        {
            if (neighbours.ContainsKey(enter) && !neighbours[enter].GetIsolated() && neighbours[enter].IsEmpty())
            {
                neighbours[enter].Push(enter, w, remainedStrength);
            }
        }

        // Sets the field's friction to the given value
        public void SetFriction(double friction)
        {
            this.friction = friction;
        }


        // Prints the field's information to the console
        public void Print()
        {
            Console.WriteLine("(" + coordX + ";" + coordY + ") " + isIsolated + " " + friction);
        }

        public void PrintFeature(string s)
        {
            if (feature != null)
            {
                feature.Print(s, coordX, coordY);
            }
        }

        public void listFieldConnectives()
        {
            if (neighbours.ContainsKey(Direction.RIGHT))
            {
                if (!neighbours[Direction.RIGHT].GetIsolated() && !GetIsolated())
                {
                    Console.WriteLine("(" + coordX + ";" + coordY + ")-(" + (coordX + 1) + ";" + coordY + ")");
                }
            }
            if (neighbours.ContainsKey(Direction.DOWN))
            {
                if (!neighbours[Direction.DOWN].GetIsolated() && !GetIsolated())
                {
                    Console.WriteLine("(" + coordX + ";" + coordY + ")-(" + coordX + ";" + (coordY + 1) + ")");
                }
            }
        }

        /*public void draw()
        {
            string left;
            string right;
            string top;
            string bot;
            string mid;

            if (!isIsolated)
            {
                if (neighbours.ContainsKey(Direction.LEFT) && !neighbours[Direction.LEFT].GetIsolated())
                {
                    left = "imageSet/verticalFloor.png";
                }
                else
                {
                    left = "imageSet/verticalBlack.png";
                }

                if (neighbours.ContainsKey(Direction.RIGHT) && !neighbours[Direction.RIGHT].GetIsolated())
                {
                    right = "imageSet/verticalFloor.png";
                }
                else
                {
                    right = "imageSet/verticalBlack.png";
                }

                if (neighbours.ContainsKey(Direction.UP) && !neighbours[Direction.UP].GetIsolated())
                {
                    top = "imageSet/horizontalFloor.png";
                }
                else
                {
                    top = "imageSet/horizontalBlack.png";
                }

                if (neighbours.ContainsKey(Direction.DOWN) && !neighbours[Direction.DOWN].GetIsolated())
                {
                    bot = "imageSet/horizontalFloor.png";
                }
                else
                {
                    bot = "imageSet/horizontalBlack.png";
                }
            }
            else
            {
                left = "imageSet/verticalBlack.png";
                right = "imageSet/verticalBlack.png";
                top = "imageSet/horizontalBlack.png";
                bot = "imageSet/horizontalBlack.png";
            }

            if (onThis == null)
            {
                if (feature == null)
                {
                    if (friction != 1)
                    {
                        if (friction < 1)
                        {
                            mid = "imageSet/oil.png";
                        }
                        else
                        {
                            mid = "imageSet/honey.png";
                        }
                    }
                    else
                    {
                        mid = "imageSet/floor.png";
                    }
                }
                else
                {
                    mid = feature.GetFeatureString();
                }
            }
            else
            {
                if (friction != 1)
                {
                    if (friction < 1)
                    {
                        if (onThis.GetMoveableString().Contains("blue"))
                        {
                            mid = "imageSet/blueOil.png";
                        }
                        else if (onThis.GetMoveableString().Contains("red"))
                        {
                            mid = "imageSet/redOil.png";
                        }
                        else
                        {
                            mid = onThis.GetMoveableString();
                        }
                    }
                    else
                    {
                        if (onThis.GetMoveableString().Contains("blue"))
                        {
                            mid = "imageSet/blueHoney.png";
                        }
                        else if (onThis.GetMoveableString().Contains("red"))
                        {
                            mid = "imageSet/redHoney.png";
                        }
                        else
                        {
                            mid = onThis.GetMoveableString();
                        }
                    }
                }
                else
                {
                    mid = onThis.GetMoveableString();
                }
            }

            /*try
            {
                jf.drawField(32 * coordX, 32 * coordY, top, bot, left, right, mid);
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }
        }*/
    }
}

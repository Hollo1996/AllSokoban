using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Features;
using Sokoban_2._0_Console.Graphics;
using Sokoban_2._0_Console.Graphics.Base;
using Sokoban_2._0_Console.Moveables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.UpperLayer.Map
{
    public class Field : MoveableVisitor
    {
        private Dictionary<Direction.dir, Field> neighbours = new Dictionary<Direction.dir, Field>();
        private Moveable onThis=null;
        private Feature feature=null;
        private Direction enter;
        private bool isIsolated=false;
        public readonly int coordLine;
        public readonly int coordColumn;
        private int friction=(Liquid.Honey.GetHashCode()+Liquid.Oil.GetHashCode())/2;
        private int remainedStrength;

        private object modifiedSync= new object();
        private bool modified=true;
        public bool Modified {
            get
            {
                lock (modifiedSync)
                    return modified;
            }
        }
        public readonly FieldRepresentation representation;

        // Constructor for the fields
        public Field(int _coordLine, int _coordColumn, FieldRepresentation _representation)
        {

            coordLine = _coordLine;
            coordColumn = _coordColumn;
            representation = _representation;
            representation.Owner = this;
        }

        // Adds the given field to its neighbours, in the given direction
        public void AddNeighbour(Field field, Direction direction)
        {
            if (!neighbours.ContainsKey(direction.direction))
            {
                neighbours.Add(direction.direction, field);
                lock(modifiedSync)
                    modified = true;
            }

        }

        // Adds a feature to the field
        public void AddFeature(Feature feature)
        {
            if (feature == null)
            {
                this.feature = feature;
                lock (modifiedSync)
                    modified = true;
            }
        }

        // Adds a Moveable object to the field
        public void AddMoveable(Moveable Moveable)
        {
            if (onThis == null)
            {
                onThis = Moveable;
                Moveable.SetField(this);
                if (feature != null)
                    feature.Interact(Moveable);
                lock (modifiedSync)
                    modified = true;
            }
        }

        // The worker calls for the LeaveRequest function when it is Moved by the player
        // If the field has a neighbor in the given direction and that field is not Isolated,
        // the worker Gets Pushed
        public void LeaveRequest(Direction direction, Worker w)
        {
            modified = true;
            if (neighbours.ContainsKey(direction.direction) && !neighbours[direction.direction].GetIsolated())
            {
                neighbours[direction.direction].Push(direction, w, w.strength);
                if (onThis == null)
                    lock (modifiedSync)
                        modified = true;
            }
        }

        // A box is being Pushed to this field
        // If this field has a Moveable object on it, the object on this field Gets Pushed to the next field, in the same direction
        // If not, the object given as a parameter will be set onto this field and Gets reMoved from its previous field
        /*public void Push(Direction direction, Box box, double remStrength)
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
        }*/

        // A worker is being Pushed to this field
        // If this field has a Moveable object on it, the worker Gets destroyed
        // After the object given as a parameter will be set onto this field and Gets reMoved from its previous field
        public void Push(Direction direction, Worker worker, int remStrength)
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
                neighbours[direction.opposite].RemoveMoveable();
                lock (modifiedSync)
                    modified = true;

                if (feature != null)
                {
                    feature.Interact(worker);
                }
            }
        }

        // ReMoves the Moveable object that is located on the field
        public void RemoveMoveable()
        {
            if (onThis != null)
            {
                onThis = null;
                lock (modifiedSync)
                    modified = true;
            }
        }

        // Destroys the Moveable object that is located on this field
        /*public void DestroyMoveable()
        {
            if (onThis != null)
            {
                onThis.Destroy();
            }
        }*/

        // Isolating the field from its neighbours by changing the isIsolated attribute
        public void Isolate()
        {
            if (isIsolated != true)
            {
                isIsolated = true;
                lock (modifiedSync)
                    modified = true;
            }
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
        /*public void Visit(Box b)
        {
            if (neighbours.ContainsKey(enter) && !neighbours[enter].GetIsolated())
            {
                neighbours[enter].Push(enter, b, remainedStrength);
            }
        }*/

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a worker
        // If this field has a neighbor in the previously stored (enter) direction and that neighbor is not Isolated and it's empty
        // the worker Gets Pushed to the next field
        public void Visit(Worker w)
        {
            if (neighbours.ContainsKey(enter.direction) && !neighbours[enter.direction].GetIsolated() && neighbours[enter.direction].IsEmpty())
            {
                neighbours[enter.direction].Push(enter, w, remainedStrength);
            }
        }

        // Sets the field's friction to the given value
        public void SetFriction(int _friction)
        {
            if (friction != _friction)
            {
                friction = _friction;
                lock (modifiedSync)
                    modified = true;
            }
        }


        // Prints the field's information to the console
        public void Print()
        {
            Console.WriteLine("(" + coordLine + ";" + coordColumn + ") " + isIsolated + " " + friction);
        }

        public void PrintFeature(string s)
        {
            if (feature != null)
            {
                feature.Print(s, coordLine, coordColumn);
            }
        }

        public void listFieldConnectives()
        {
            if (neighbours.ContainsKey(Direction.dir.RIGHT))
            {
                if (!neighbours[Direction.dir.RIGHT].GetIsolated() && !GetIsolated())
                {
                    Console.WriteLine("(" + coordLine + ";" + coordColumn + ")-(" + (coordLine + 1) + ";" + coordColumn + ")");
                }
            }
            if (neighbours.ContainsKey(Direction.dir.DOWN))
            {
                if (!neighbours[Direction.dir.DOWN].GetIsolated() && !GetIsolated())
                {
                    Console.WriteLine("(" + coordLine + ";" + coordColumn + ")-(" + coordLine + ";" + (coordColumn + 1) + ")");
                }
            }
        }

        public void LoadRepresentation()
        {
            representation.OnOwner = onThis;
            representation._Feature = feature;
            representation.Friction = friction;
            representation.Neighbours = neighbours;
            lock (modifiedSync)
                modified = false;
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
                if (neighbours.ContainsKey(Direction.dir.LEFT) && !neighbours[Direction.dir.LEFT].GetIsolated())
                {
                    left = "imageSet/verticalFloor.png";
                }
                else
                {
                    left = "imageSet/verticalBlack.png";
                }

                if (neighbours.ContainsKey(Direction.dir.RIGHT) && !neighbours[Direction.dir.RIGHT].GetIsolated())
                {
                    right = "imageSet/verticalFloor.png";
                }
                else
                {
                    right = "imageSet/verticalBlack.png";
                }

                if (neighbours.ContainsKey(Direction.dir.UP) && !neighbours[Direction.dir.UP].GetIsolated())
                {
                    top = "imageSet/horizontalFloor.png";
                }
                else
                {
                    top = "imageSet/horizontalBlack.png";
                }

                if (neighbours.ContainsKey(Direction.dir.DOWN) && !neighbours[Direction.dir.DOWN].GetIsolated())
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

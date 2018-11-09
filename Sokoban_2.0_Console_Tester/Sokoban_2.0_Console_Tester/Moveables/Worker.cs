using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sokoban_2._0_Console_Tester
{
    public class Worker : Moveable
    {
        private Direction inDirection = Direction.UP;
        private ControlInterface owner;
        private Field underThis;
        public readonly double strength;

        // Constructor for the workers
        public Worker(double strength)
        {
            this.strength = strength;
        }

        // The player pushed the worker in a direction,
        // the field's leaveRequest function  is responsible for handling this
        public void Move(Direction direction)
        {
            inDirection = direction;
            underThis.LeaveRequest(direction, this);
        }


        // Returns the worker's color
        public Color GetColor()
        {
            return owner.id;
        }

        // Visitor pattern function
        // Calls the Visitor's Visit function and gives itself as a parameter
        public override void Accept(MoveableVisitor v)
        {
            v.Visit(this);
        }

        // Destroys the box by removing it from the field and from the game control interface
        public override  void Destroy()
        {
            underThis.RemoveMoveable();
            owner.RemoveWorker();
        }

        public override string GetMoveableString()
        {
            throw new NotImplementedException();
        }

        // The worker is being pushed by a box to a field
        // If the given field does not exist, is Isolated or isn't empty, the worker Gets destroyed
        public override void Pushed(Box by, Field to)
        {
            if (to == null || to.GetIsolated() || !to.IsEmpty())
            {
                underThis.DestroyMoveable();
            }
        }

        // The worker is being pushed by a worker to a field
        // Does nothing special, implemented in the field's push function
        public override void Pushed(Worker by, Field to)
        {
        }

        // Sets the field's friction to the given amount
        // 0 < X < 1 : oil-ish
        // X == 1    : natural
        // X > 1     : sticky
        public void AddLiquid(double liquid)
        {
            if (liquid > 0)
            {
                underThis.SetFriction(liquid);
            }
        }

        // Sets the worker's underThis attribute to the given field
        public override void SetField(Field field)
        {
            underThis = field;
        }

        // Sets an owner for the worker, Gets the control interface as a parameter
        public void SetOwner(ControlInterface controlInterface)
        {
            if (this.owner == null)
                this.owner = controlInterface;
        }

        // Prints out the worker's attributes to the console
        public void Print(int point)
        {
            Console.WriteLine("(" + underThis.coordX + ";" + underThis.coordY + ") " + strength + " " + inDirection + " " + point);
        }

        public string GetMoveablestring()
        {
            Color c = owner.id;
            if (c.Equals(Color.Blue))
            {
                return "imageSet/bluePlayer.png";
            }
            else if (c.Equals(Color.Red))
            {
                return "imageSet/redPlayer.png";
            }
            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console
{
    public class Box : Moveable
    {
        public Color pushedByColor;
        private Field underThis;

        // Isolates the box, by isolating the field this is under the box
        public void Isolate()
        {
            underThis.Isolate();
        }

        // Visitor pattern function
        // Calls the Visitor's Visit function and gives itself as a parameter
        public override void Accept(MoveableVisitor v)
        {
            v.Visit(this);
        }

        // Destroys the box by removing it from the field and from the game field
        public override void Destroy()
        {
            underThis.RemoveMoveable();
            GameField.GetInstance().RemoveBox(this);
        }

        public override string GetMoveableString()
        {
            throw new NotImplementedException();
        }

        
        // The box is being pushed by a box to a field
        // Does nothing special, implemented in the field's push function
        public override void Pushed(Box by, Field to)
        {
            pushedByColor = by.pushedByColor;
        }

        // The box is being pushed by a worker to a field
        // Stores the worker's color as an attribute, so if the box Gets pushed to a container, we know who pushed it there
        public override void Pushed(Worker by, Field to)
        {
            this.pushedByColor = by.GetColor();
        }

        // Sets the box's underThis attribute to the given field
        public override void SetField(Field field)
        {
            underThis = field;
        }

        public void Print()
        {
            Console.WriteLine("(" + underThis.coordX + ";" + underThis.coordY + ") " + pushedByColor.Name);
        }
    }

    /*public string GetMoveableString()
    {
        if (pushedByColor.Equals(Color.Blue))
        {
            return "imageSet/bluePlayer.png";
        }
        else if (pushedByColor.Equals(Color.Red))
        {
            return "imageSet/redPlayer.png";
        }
        return "";
    }*/
}

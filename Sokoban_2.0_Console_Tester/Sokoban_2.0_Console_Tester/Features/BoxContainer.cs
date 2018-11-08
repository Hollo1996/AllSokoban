using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    public class BoxContainer : Feature, MoveableVisitor
    {
        private List<Box> compatible = new List<Box>();

        // Adds a box to the compatible list
        public void AddBox(Box b)
        {
            compatible.Add(b);
        }

        // Interacting with the Moveable object
        // Initiating the Visitor pattern
        public void Interact(Moveable Moveable)
        {
            Moveable.Accept(this);
        }

        public void Print(string s, int x, int y)
        {
            if (s.Equals("bc"))
            {
                Console.WriteLine("(" + x + ";" + y + ") " + compatible.Count());
            }
        }

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a Box
        // If the box is compatible, then isolating and signaling to the game field, to increment score
        public void Visit(Box b)
        {
            if (compatible.Contains(b))
            {
                b.Isolate();
                GameField.GetInstance().Score(b.pushedByColor);
            }
        }

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a Worker, nothing happens
        public void Visit(Worker b)
        {
        }

        public string GetFeatureString()
        {
            return "imageSet/boxContainer.png";
        }
    }
}

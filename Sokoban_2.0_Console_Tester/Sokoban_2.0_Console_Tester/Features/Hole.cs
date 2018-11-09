using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    public class Hole : Feature
    {
        public virtual string GetFeatureString()
        {
            return "imageSet/hole.png";
        }

        // Interacting with the Moveable object
        // The Moveable object needs to be destroyed
        public virtual void Interact(Moveable Moveable)
        {
            Moveable.Destroy();
        }

        public virtual void Print(string s, int x, int y)
        {
            if (s.Equals("ho"))
            {
               Console.WriteLine("(" + x + ";" + y + ")");
            }
        }
    }
}

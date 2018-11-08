using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Consol_Tester
{
    public class FallTrap : Hole, Switchable
    {
        private int switched;
        private Field masterField;

        // Constructor for the fall trap, initializing attributes
        public FallTrap(Field master)
        {
            switched = 0;
            masterField = master;
        }

        // Interacting with the Moveable object
        // If the switched attribute is greater than 0, that means the fall trap is open,
        // so the Moveable object needs to be destroyed
        public override void Interact(Moveable Moveable)
        {
            if (switched > 0)
            {
                Moveable.Destroy();
            }
        }

        // Switching off the fall trap by decrementing the switched attribute
        public void SwitchOff()
        {
            switched--;
        }

        // Switching on the fall trap by incrementing the switched attribute
        // and calling the master field to destroy the Moveable object on this field
        // If it was already switched on, we don't have to that
        public void SwitchOn()
        {
            switched++;
            if (switched == 1)
            {
                masterField.DestroyMoveable();
            }
        }

        public override void Print(string s, int x, int y)
        {
            if (s.Equals("ft"))
            {
                Console.WriteLine("(" + x + ";" + y + ") " + switched);
            }
        }

        public Field GetMasterField()
        {
            return masterField;
        }

        public override string GetFeatureString()
        {
            if (switched > 0) return "imageSet/hole.png";
            else { return "imageSet/fallTrap.png"; }
        }
    }
}

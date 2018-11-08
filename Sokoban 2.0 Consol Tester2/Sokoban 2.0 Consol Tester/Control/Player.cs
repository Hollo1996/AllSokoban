using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Consol_Tester
{
    public class Player
    {

        private string name;
        private ControlInterface controlInterface;

        // Constructor for the player
        public Player(string name)
        {
            this.name = name;
        }

        public void lost()
        {
        }

        public void won()
        {
        }

        // Adds the given control interface to the player
        public void AddControl(ControlInterface controlInterface)
        {
            this.controlInterface = controlInterface;
        }

        public ControlInterface GetCI()
        {
            return controlInterface;
        }

        public string GetName()
        {
            return name;
        }
    }
}

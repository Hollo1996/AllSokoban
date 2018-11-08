using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    public class Event
    {
        private string keyPressed;
        private double liquid = 0;

        public Event(string keyPressed)
        {
            this.keyPressed = keyPressed;
        }

        public Event(string keyPressed, double liquid)
        {
            this.keyPressed = keyPressed;
            this.liquid = liquid;
        }

        public double GetLiquid()
        {
            return liquid;
        }

        public string GetKeyPressed()
        {
            return keyPressed;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    public class WasdControlInterface : ControlInterface
    {
        public WasdControlInterface(Color color, Worker w) :base(color, w, "w", "d", "s", "a", "p")
        { }


        public override void OnEvent(Event e)
        {
            string keyPressed = e.GetKeyPressed();
            switch (keyPressed)
            {
                case "A":
                    this.worker.Move(Direction.LEFT);
                    break;
                case "D":
                    this.worker.Move(Direction.RIGHT);
                    break;
                case "W":
                    this.worker.Move(Direction.UP);
                    break;
                case "S":
                    this.worker.Move(Direction.DOWN);
                    break;
            }
        }
    }
}

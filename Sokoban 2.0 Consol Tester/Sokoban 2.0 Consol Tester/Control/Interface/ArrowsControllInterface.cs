using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Consol_Tester
{
    public class ArrowsControlInterface : ControlInterface
    {
        public ArrowsControlInterface(Color color, Worker w) : base(color, w, "arUp", "arRight", "arDown", "arLeft", "space")
        {}

        public override void OnEvent(Event e)
        { // case statements temporary, only for proto
            string keyPressed = e.GetKeyPressed();
            switch (keyPressed)
            {
                case "left":
                    this.worker.Move(Direction.LEFT);
                    break;
                case "right":
                    this.worker.Move(Direction.RIGHT);
                    break;
                case "up":
                    this.worker.Move(Direction.UP);
                    break;
                case "down":
                    this.worker.Move(Direction.DOWN);
                    break;
            }
        }
    }
}

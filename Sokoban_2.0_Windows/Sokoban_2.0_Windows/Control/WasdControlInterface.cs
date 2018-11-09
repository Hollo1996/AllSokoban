using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Windows
{
    class WasdControlInterface
    {
    }
}

/*package hu.bme.sch.sokoban.game.controls;

import hu.bme.sch.sokoban.Event;
import hu.bme.sch.sokoban.game.commons.Direction;
import hu.bme.sch.sokoban.game.entities.Worker;

import java.awt.*;

public class WasdControlInterface extends ControlInterface {
    public WasdControlInterface(Color color, Worker w) {
        super(color, w,"w", "d", "s", "a","p");
    }

    @Override
    public void onEvent(Event event) {
        String keyPressed = event.getKeyPressed();
        switch (keyPressed) {
            case "A":
                this.worker.move(Direction.LEFT);
                break;
            case "D":
                this.worker.move(Direction.RIGHT);
                break;
            case "W":
                this.worker.move(Direction.UP);
                break;
            case "S":
                this.worker.move(Direction.DOWN);
                break;
        }
    }
}
*/

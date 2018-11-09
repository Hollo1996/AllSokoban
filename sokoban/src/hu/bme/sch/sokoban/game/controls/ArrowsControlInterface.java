package hu.bme.sch.sokoban.game.controls;

import hu.bme.sch.sokoban.Event;
import hu.bme.sch.sokoban.game.commons.Direction;
import hu.bme.sch.sokoban.game.entities.Worker;

import java.awt.*;

public class ArrowsControlInterface extends ControlInterface {
    public ArrowsControlInterface(Color color, Worker w) {
        super(color, w,"arUp","arRight","arDown","arLeft","space");
    }

    @Override
    public void onEvent(Event event) { // case statements temporary, only for proto
        String keyPressed = event.getKeyPressed();
        switch (keyPressed) {
            case "left":
                this.worker.move(Direction.LEFT);
                break;
            case "right":
                this.worker.move(Direction.RIGHT);
                break;
            case "up":
                this.worker.move(Direction.UP);
                break;
            case "down":
                this.worker.move(Direction.DOWN);
                break;
        }
    }
}

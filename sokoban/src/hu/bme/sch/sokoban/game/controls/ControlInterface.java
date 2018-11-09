package hu.bme.sch.sokoban.game.controls;

import hu.bme.sch.sokoban.Event;
import hu.bme.sch.sokoban.game.entities.Worker;
import hu.bme.sch.sokoban.game.commons.Direction;
import hu.bme.sch.sokoban.game.map.GameField;

import java.awt.*;

public class ControlInterface {
    public final Color       id;
    protected    Worker      worker;

    private String UP, RIGHT, DOWN, LEFT, PUT_LIQUID;

    // Constructor for the control interface
    public ControlInterface(Color color, Worker w, String UP, String RIGHT, String DOWN, String LEFT, String PUT_LIQUID) {
        this.id         = color;
        this.worker     = w;
        w.setOwner(this);

        this.UP = UP;
        this.RIGHT = RIGHT;
        this.DOWN = DOWN;
        this.LEFT = LEFT;
        this.PUT_LIQUID = PUT_LIQUID;
    }

    public String getUP() {
        return UP;
    }

    public String getRIGHT() {
        return RIGHT;
    }

    public String getDOWN() {
        return DOWN;
    }

    public String getLEFT() {
        return LEFT;
    }

    public String getPUT_LIQUID() {
        return PUT_LIQUID;
    }

    // Returns the control interface's identifier
    public Color getId() {
        return id;
    }

    // The players moves his worker in a given direction
    public void move(Direction d) {
        worker.move(d);
    }

    // Removes the worker from the control interface
    // and signals this action to the game field
    public void removeWorker() {
        worker = null;
        GameField.getInstance().outOfWorkers();
    }

    // Return weather the control interface has a worker or not
    public boolean hasWorker() {
        return worker != null;
    }

    // Called by GameField on keypress event
    public void onEvent(Event event) {
        String keyPressed = event.getKeyPressed();

        if(keyPressed.equals(UP)) {
            this.worker.move(Direction.UP);
        }
        if(keyPressed.equals(RIGHT)) {
            this.worker.move(Direction.RIGHT);
        }
        if(keyPressed.equals(DOWN)) {
            this.worker.move(Direction.DOWN);
        }
        if(keyPressed.equals(LEFT)) {
            this.worker.move(Direction.LEFT);
        }
        if(keyPressed.equals(PUT_LIQUID)) {
            this.worker.putLiquid(event.getLiquid());
        }
    }

    public void listWorker(int point){
        if(worker == null) {
            return;
        }

        worker.print(point);
    }
}

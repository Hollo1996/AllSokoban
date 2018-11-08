using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0
{
    public class Field
    {
    }
}

/*
 package hu.bme.sch.sokoban.game.map;

import hu.bme.sch.sokoban.game.commons.Direction;
import hu.bme.sch.sokoban.game.entities.Box;
import hu.bme.sch.sokoban.game.entities.Movable;
import hu.bme.sch.sokoban.game.entities.Worker;
import hu.bme.sch.sokoban.game.features.Feature;
import hu.bme.sch.sokoban.game.features.MovableVisitor;
import hu.bme.sch.sokoban.game.graphics.SokobanView;

import java.io.File;
import java.io.IOException;
import java.util.HashMap;

public class Field implements MovableVisitor {
    private HashMap<Direction, Field> neighbours = new HashMap<>();
    private Movable onThis;
    private Feature feature;
    private Direction enter;
    private boolean isIsolated;
    public final int coordX;
    public final int coordY;
    private double friction;
    private double remainedStrength;

    // Constructor for the fields
    public Field(int x, int y) {
        coordX = x;
        coordY = y;
        isIsolated = false;
    }

    // Adds the given field to its neighbours, in the given direction
    public void addNeighbour(Field field, Direction direction) {
        neighbours.put(direction, field);
    }

    // Adds a feature to the field
    public void addFeature(Feature feature) {
        this.feature = feature;
    }

    // Adds a movable object to the field
    public void addMovable(Movable movable) {
        onThis = movable;
        movable.setField(this);
        if (feature != null)
            feature.interact(movable);
    }

    // Removes the movable object that is located on the field
    public void removeMovable() {
        onThis = null;
    }

    // Destroys the movable object that is located on this field
    public void destroyMovable() {
        if (onThis != null) {
            onThis.destroy();
        }
    }

    // Isolating the field from its neighbours by changing the isIsolated attribute
    public void isolate() {
        isIsolated = true;
    }

    // Returns the value stored in the isIsolated attribute
    public boolean getIsolated() {
        return isIsolated;
    }

    // Returns whether the field is empty or not
    public boolean isEmpty() {
        return onThis == null;
    }

    // The worker calls for the leaveRequest function when it is moved by the player
    // If the field has a neighbor in the given direction and that field is not isolated,
    // the worker gets pushed
    public void leaveRequest(Direction direction, Worker w) {
        if (neighbours.containsKey(direction) && !neighbours.get(direction).getIsolated()) {
            neighbours.get(direction).push(direction, w, w.strength);
        }
    }

    // A box is being pushed to this field
    // If this field has a movable object on it, the object on this field gets pushed to the next field, in the same direction
    // If not, the object given as a parameter will be set onto this field and gets removed from its previous field
    public void push(Direction direction, Box box, double remStrength) {
        if (onThis != null) {
            onThis.pushed(box, neighbours.getOrDefault(direction, null));

            if (onThis != null && ((remStrength - friction) > 0)) {
                enter = direction;
                remainedStrength = remStrength - friction;
                onThis.accept(this);
            }
        }
        if (onThis == null) {
            onThis = box;
            box.setField(this);
            neighbours.get(direction.getReverse()).removeMovable();

            if (feature != null) {
                feature.interact(box);
            }
        }
    }

    // A worker is being pushed to this field
    // If this field has a movable object on it, the worker gets destroyed
    // After the object given as a parameter will be set onto this field and gets removed from its previous field
    public void push(Direction direction, Worker worker, double remStrength) {
        if (onThis != null) {
            onThis.pushed(worker, null);

            if (onThis != null && ((remStrength - friction) > 0)) {
                enter = direction;
                remainedStrength = remStrength - friction;
                onThis.accept(this);
            }
        }

        if (onThis == null) {
            onThis = worker;
            worker.setField(this);
            neighbours.get(direction.getReverse()).removeMovable();

            if (feature != null) {
                feature.interact(worker);
            }
        }
    }

    // Visitor pattern core, gets the given Movable type as argument
    // In this case, the given object is a box
    // If this field has a neighbor in the previously stored (enter) direction and that neighbor is not isolated,
    // the box gets pushed to the next field
    @Override
    public void visit(Box b) {
        if (neighbours.containsKey(enter) && !neighbours.get(enter).getIsolated()) {
            neighbours.get(enter).push(enter, b, remainedStrength);
        }
    }

    // Visitor pattern core, gets the given Movable type as argument
    // In this case, the given object is a worker
    // If this field has a neighbor in the previously stored (enter) direction and that neighbor is not isolated and it's empty
    // the worker gets pushed to the next field
    @Override
    public void visit(Worker w) {
        if (neighbours.containsKey(enter) && !neighbours.get(enter).getIsolated() && neighbours.get(enter).isEmpty()) {
            neighbours.get(enter).push(enter, w, remainedStrength);
        }
    }

    // Sets the field's friction to the given value
    public void setFriction(double friction) {
        this.friction = friction;
    }

    // Prints the field's information to the console
    public void print() {
        System.out.println("(" + coordX + ";" + coordY + ") " + isIsolated + " " + friction);
    }

    public void printFeature(String s) {
        if (feature != null) {
            feature.print(s, coordX, coordY);
        }
    }

    public void listFieldConnectives() {
        if (neighbours.containsKey(Direction.RIGHT)) {
            if (!neighbours.get(Direction.RIGHT).getIsolated() && !getIsolated()) {
                System.out.println("(" + coordX + ";" + coordY + ")-(" + (coordX + 1) + ";" + coordY + ")");
            }
        }
        if (neighbours.containsKey(Direction.DOWN)) {
            if (!neighbours.get(Direction.DOWN).getIsolated() && !getIsolated()) {
                System.out.println("(" + coordX + ";" + coordY + ")-(" + coordX + ";" + (coordY + 1) + ")");
            }
        }
    }

    public void draw(SokobanView jf){
        String left;
        String right;
        String top;
        String bot;
        String mid;

        if(!isIsolated){
            if(neighbours.containsKey(Direction.LEFT) && !neighbours.get(Direction.LEFT).getIsolated()) {
                left = "imageSet" + File.separator + "verticalFloor.png";
            }
            else {
                left = "imageSet" + File.separator + "verticalBlack.png";
            }

            if(neighbours.containsKey(Direction.RIGHT) && !neighbours.get(Direction.RIGHT).getIsolated()) {
                right = "imageSet" + File.separator + "verticalFloor.png";
            }
            else {
                right = "imageSet" + File.separator + "verticalBlack.png";
            }

            if(neighbours.containsKey(Direction.UP) && !neighbours.get(Direction.UP).getIsolated()) {
                top = "imageSet" + File.separator + "horizontalFloor.png";
            }
            else {
                top = "imageSet" + File.separator + "horizontalBlack.png";
            }

            if(neighbours.containsKey(Direction.DOWN) && !neighbours.get(Direction.DOWN).getIsolated()) {
                bot = "imageSet" + File.separator + "horizontalFloor.png";
            }
            else {
                bot = "imageSet" + File.separator + "horizontalBlack.png";
            }
        }
        else {
            left = "imageSet" + File.separator + "verticalBlack.png";
            right = "imageSet" + File.separator + "verticalBlack.png";
            top = "imageSet" + File.separator + "horizontalBlack.png";
            bot = "imageSet" + File.separator + "horizontalBlack.png";
        }

        if(onThis == null) {
            if (feature == null) {
                if(friction != 1) {
                    if(friction < 1) {
                        mid = "imageSet" + File.separator + "oil.png";
                    } else {
                        mid = "imageSet" + File.separator + "honey.png";
                    }
                } else {
                    mid = "imageSet" + File.separator + "floor.png";
                }
            }
            else {
                mid = feature.getFeatureString();
            }
        }
        else {
            if(friction != 1) {
                if(friction < 1) {
                    if(onThis.getMoveableString().contains("blue")) {
                        mid = "imageSet" + File.separator + "blueOil.png";
                    } else if(onThis.getMoveableString().contains("red")) {
                        mid = "imageSet" + File.separator + "redOil.png";
                    } else {
                        mid = onThis.getMoveableString();
                    }
                } else {
                    if(onThis.getMoveableString().contains("blue")) {
                        mid = "imageSet" + File.separator + "blueHoney.png";
                    } else if(onThis.getMoveableString().contains("red")) {
                        mid = "imageSet" + File.separator + "redHoney.png";
                    } else {
                        mid = onThis.getMoveableString();
                    }
                }
            } else {
                mid = onThis.getMoveableString();
            }
        }

        try {
            jf.drawField(32*coordX, 32*coordY, top, bot, left, right, mid);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
*/

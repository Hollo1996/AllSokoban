package hu.bme.sch.sokoban.game.entities;

import hu.bme.sch.sokoban.game.map.Field;
import hu.bme.sch.sokoban.game.features.MovableVisitor;

public abstract class Movable {
    // Stores the given Field for the movable object as an attribute called "underThis"
    public abstract void setField(Field field);

    // Destroys the movable object
    public abstract void destroy();

    // Visitor pattern function
    public abstract void accept(MovableVisitor v);

    // A movable object gets pushed by a box, to a given field
    public abstract void pushed(Box by, Field to);

    // A movable object gets pushed by a worker, to a given field
    public abstract void pushed(Worker by, Field to);

    public abstract String getMoveableString();
}

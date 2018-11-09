package hu.bme.sch.sokoban.game.features;

import hu.bme.sch.sokoban.game.entities.*;

public interface MovableVisitor {

    // Visitor pattern core, gets the given Movable type as argument
    void visit(Box b);
    void visit(Worker b);
}

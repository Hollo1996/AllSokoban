package hu.bme.sch.sokoban.game.features;

import hu.bme.sch.sokoban.game.entities.Movable;

public interface Feature {
    // Interacting with the movable object that gets on any type of feature
    void interact(Movable movable);

    void print(String s, int x, int y);

    String getFeatureString();
}
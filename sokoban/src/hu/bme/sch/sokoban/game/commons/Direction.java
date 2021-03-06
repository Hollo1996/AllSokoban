package hu.bme.sch.sokoban.game.commons;

public enum Direction {
    // The 4 Direction  we can choose
    UP, DOWN, LEFT, RIGHT;

    // Each direction has an opposite direction, these opposites are declared here
    private Direction opposite;
    static {
        UP.opposite = DOWN;
        DOWN.opposite = UP;
        RIGHT.opposite = LEFT;
        LEFT.opposite = RIGHT;
    }

    // A function that returns the opposite for a certain direction
    public Direction getReverse() {
        return opposite;
    }
}
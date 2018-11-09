package hu.bme.sch.sokoban;

public class Event {
    private String keyPressed;
    private double liquid = 0;

    public Event(String keyPressed) {
        this.keyPressed = keyPressed;
    }

    public Event(String keyPressed, double liquid) {
        this.keyPressed = keyPressed;
        this.liquid = liquid;
    }

    public double getLiquid() {
        return liquid;
    }

    public String getKeyPressed() {
        return keyPressed;
    }
}

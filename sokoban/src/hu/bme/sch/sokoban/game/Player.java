package hu.bme.sch.sokoban.game;

import hu.bme.sch.sokoban.game.controls.ControlInterface;

public class Player {
    private String name;
    private ControlInterface controlInterface;

    // Constructor for the player
    public Player(String name){
        this.name = name;
    }

    public void lost() {
    }

    public void won() {
    }

    // Adds the given control interface to the player
    public void addControl(ControlInterface controlInterface) {
        this.controlInterface = controlInterface;
    }
    
    public ControlInterface getCI() {
    	return controlInterface;
    }

    public String getName() {
        return name;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Windows
{
    class Button
    {
    }
}

/*
 package hu.bme.sch.sokoban.game.features;

import hu.bme.sch.sokoban.game.entities.*;

import java.io.File;
import java.util.ArrayList;

public class Button implements Feature, MovableVisitor {
    private boolean                 isPushed;
    private ArrayList<Switchable>   traps = new ArrayList<>();

    // Adds a switchable type (trap) to the list
    public void addSwitchable(Switchable switchable) {
        traps.add(switchable);
    }

    // Interacting with the movable object
    // Initiating the visitor pattern
    @Override
    public void interact(Movable movable) {
        movable.accept(this);
    }

    // Visitor pattern core, gets the given Movable type as argument
    // In this case, the given object is a Box
    // If the button was not pushed before, we push it and switch on every trap
    public void visit(Box b) {
        if(!isPushed){
            traps.forEach(Switchable::switchOn);
            isPushed = true;
        }
    }

    // Visitor pattern core, gets the given Movable type as argument
    // In this case, the given object is a Worker, nothing happens
    public void visit(Worker w) {
        if (isPushed) {
            traps.forEach(Switchable::switchOff);
            isPushed = false;
        }
    }

    public void print(String s, int x, int y){
        if(s.equals("bu")) {
            System.out.println("("+ x + ";" + y + ") " + isPushed);
        }
        else if (s.equals("buttonConnectives")){
            for (Switchable switchable: traps){
                FallTrap ft = (FallTrap)switchable;
                System.out.println("("+ x + ";" + y + ")-(" + ft.getMasterField().coordX + ";" + ft.getMasterField().coordY + ")");
            }
        }
    }

    public String getFeatureString(){
        return "imageSet" + File.separator + "button.png";
    }
}*/

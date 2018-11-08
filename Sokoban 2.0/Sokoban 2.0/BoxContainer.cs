using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0
{
    class BoxContainer
    {
    }
}

/*
 package hu.bme.sch.sokoban.game.features;

import hu.bme.sch.sokoban.game.entities.Worker;
import hu.bme.sch.sokoban.game.map.GameField;
import hu.bme.sch.sokoban.game.entities.Box;
import hu.bme.sch.sokoban.game.entities.Movable;

import java.io.File;
import java.lang.reflect.Array;
import java.util.ArrayList;

public class BoxContainer implements Feature, MovableVisitor {
    private ArrayList<Box>  compatible = new ArrayList<>();

    // Adds a box to the compatible list
    public void addBox(Box b){
        compatible.add(b);
    }

    // Interacting with the movable object
    // Initiating the visitor pattern
    @Override
    public void interact(Movable movable) {
        movable.accept(this);
    }

    // Visitor pattern core, gets the given Movable type as argument
    // In this case, the given object is a Box
    // If the box is compatible, then isolating and signaling to the game field, to increment score
    @Override
    public void visit(Box b) {
        if( compatible.contains(b) ){
            b.isolate();
            GameField.getInstance().score(b.pushedByColor);
        }
    }

    // Visitor pattern core, gets the given Movable type as argument
    // In this case, the given object is a Worker, nothing happens
    @Override
    public void visit(Worker b) {
    }

    public void print(String s, int x, int y){
        if(s.equals("bc")){
            System.out.println("(" + x + ";" + y + ") " + compatible.size());
        }
    }

    public String getFeatureString(){
        return "imageSet" + File.separator + "boxContainer.png";
    }
}
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0
{
    class FallTrap
    {
    }
}

/*package hu.bme.sch.sokoban.game.features;

import hu.bme.sch.sokoban.game.map.Field;
import hu.bme.sch.sokoban.game.entities.Movable;

import java.io.File;

public class FallTrap extends Hole implements Switchable, Feature {
    private int      switched;
    private Field    masterField;

    // Constructor for the fall trap, initializing attributes
    public FallTrap(Field master){
        switched = 0;
        masterField = master;
    }

    // Switching on the fall trap by incrementing the switched attribute
    // and calling the master field to destroy the movable object on this field
    // If it was already switched on, we don't have to that
    @Override
    public void switchOn() {
        switched++;
        if (switched==1){
            masterField.destroyMovable();
        }
    }

    // Switching off the fall trap by decrementing the switched attribute
    @Override
    public void switchOff() {
        switched--;
    }

    // Interacting with the movable object
    // If the switched attribute is greater than 0, that means the fall trap is open,
    // so the movable object needs to be destroyed
    @Override
    public void interact(Movable movable) {
        if(switched > 0){
            movable.destroy();
        }
    }

    public void print(String s, int x, int y){
        if (s.equals("ft")){
            System.out.println("(" + x + ";" + y + ") " + switched);
        }
    }

    public Field getMasterField(){
        return masterField;
    }

    public String getFeatureString(){
        if(switched > 0) return "imageSet" + File.separator + "hole.png";
        else { return "imageSet" + File.separator + "fallTrap.png";}
    }
}*/

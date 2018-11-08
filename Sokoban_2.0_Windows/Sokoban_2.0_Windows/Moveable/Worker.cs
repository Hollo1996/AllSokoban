using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Windows
{
    public class Worker
    {
    }
}

/*
 package hu.bme.sch.sokoban.game.entities;

import hu.bme.sch.sokoban.game.controls.ControlInterface;
import hu.bme.sch.sokoban.game.commons.Direction;
import hu.bme.sch.sokoban.game.map.Field;
import hu.bme.sch.sokoban.game.features.MovableVisitor;

import java.awt.*;
import java.io.File;

public class Worker extends Movable{
    private Direction           inDirection = Direction.UP; //?
    private ControlInterface    owner;
    private Field               underThis;
    public final double         strength;

    // Constructor for the workers
    public Worker(double strength){
        this.strength = strength;
    }

    // The player pushed the worker in a direction,
    // the field's leaveRequest function  is responsible for handling this
    public void move(Direction direction) {
        inDirection = direction;
        underThis.leaveRequest(direction, this);
    }

    // Returns the worker's color
    public Color getColor() {
        return owner.id;
    }

    // Destroys the box by removing it from the field and from the game control interface
    @Override
    public void destroy() {
        underThis.removeMovable();
        owner.removeWorker();
    }

    // Sets the worker's underThis attribute to the given field
    @Override
    public void setField(Field field) {
        underThis = field;
    }

    // Visitor pattern function
    // Calls the visitor's visit function and gives itself as a parameter
    @Override
    public void accept(MovableVisitor v) {
        v.visit(this);
    }

    // The worker is being pushed by a box to a field
    // If the given field does not exist, is isolated or isn't empty, the worker gets destroyed
    @Override
    public void pushed(Box by, Field to) {
        if(to == null   ||  to.getIsolated() || !to.isEmpty()){
            underThis.destroyMovable();
        }
    }

    // The worker is being pushed by a worker to a field
    // Does nothing special, implemented in the field's push function
    @Override
    public void pushed(Worker by, Field to) {
    }

    // Sets the field's friction to the given amount
    // 0 < X < 1 : oil-ish
    // X == 1    : natural
    // X > 1     : sticky
    public void putLiquid(double liquid){
        if (liquid > 0){
            underThis.setFriction(liquid);
        }
    }

    // Sets an owner for the worker, gets the control interface as a parameter
    public void setOwner(ControlInterface controlInterface) {
        if(this.owner==null)
            this.owner = controlInterface;
    }

    // Prints out the worker's attributes to the console
    public void print(int point){
        System.out.println("(" + underThis.coordX + ";" + underThis.coordY + ") " + strength + " " + inDirection + " " + point);
    }

    public String getMoveableString(){
        Color c = owner.getId();
        if(c.equals(new Color(0, 0, 255))){
            return "imageSet" + File.separator + "bluePlayer.png";
        }
        else if(c.equals(new Color(255, 0, 0))){
            return "imageSet" + File.separator + "redPlayer.png";
        }
        return "";
    }
}*/

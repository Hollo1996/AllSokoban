using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Windows
{
    public class Box : Moveable
    {
        public override void accept(MoveableVisitor v)
        {
            throw new NotImplementedException();
        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override string getMoveableString()
        {
            throw new NotImplementedException();
        }

        public override void Pushed(Box by, Field to)
        {
            throw new NotImplementedException();
        }

        public override void Pushed(Worker by, Field to)
        {
            throw new NotImplementedException();
        }

        public override void SetField(Field field)
        {
            throw new NotImplementedException();
        }
    }
}

/*
public class Box extends Movable {
    public Color pushedByColor;
    private Field       underThis;

    // Isolates the box, by isolating the field this is under the box
    public void isolate() {
        underThis.isolate();
    }

    // Destroys the box by removing it from the field and from the game field
    @Override
    public void destroy() {
        underThis.removeMovable();
        GameField.getInstance().removeBox(this);
    }

    // Sets the box's underThis attribute to the given field
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

    // The box is being pushed by a box to a field
    // Does nothing special, implemented in the field's push function
    @Override
    public void pushed(Box by, Field to) {
        pushedByColor=by.pushedByColor;
    }

    // The box is being pushed by a worker to a field
    // Stores the worker's color as an attribute, so if the box gets pushed to a container, we know who pushed it there
    @Override
    public void pushed(Worker by, Field to) {
        this.pushedByColor = by.getColor();
    }

    public void print(){
        String col="none";
        if(pushedByColor!=null)
        switch (pushedByColor.getRGB()){
            case 0xff0000ff:
                col="blue";
                break;
            case 0xffff0000:
                col="red";
                break;
            case 0xff00ff00:
                col="green";
                break;
            case 0xffffff00:
                col="yellow";
                break;
            case 0xff000000:
                col="black";
                break;
            case 0xffffffff:
                col="white";
                break;
            case 0xffffafaf:
                col="pink";
                break;
            case 0xffffc800:
                col="orange";
                break;
            case 0xffff00ff:
                col="magente";
                break;
            default:
                break;
        }
        System.out.println("(" + underThis.coordX + ";" + underThis.coordY + ") " + col);
    }

    public String getMoveableString(){
        return "imageSet" + File.separator + "box.png";
    }
}*/

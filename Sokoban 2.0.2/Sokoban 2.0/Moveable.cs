using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0
{
    public abstract class Moveable
    {
        // Stores the given Field for the movable object as an attribute called "underThis"
        public abstract void SetField(Field field);

        // Destroys the movable object
        public abstract void Destroy();

        // Visitor pattern function
        public abstract void accept(MoveableVisitor v);

        // A movable object gets pushed by a box, to a given field
        public abstract void Pushed(Box by, Field to);

        // A movable object gets pushed by a worker, to a given field
        public abstract void Pushed(Worker by, Field to);

        // Generates a String form of the class
        public abstract String getMoveableString();
    }
}

/*
public abstract class Movable {
    // Stores the given Field for the movable object as an attribute called "underThis"
    public abstract void setField(Field field);

    // Destroys the movable object
    public abstract void destroy();

    // Visitor pattern function
    public abstract void accept(MoveableVisitor v);

    // A movable object gets pushed by a box, to a given field
    public abstract void pushed(Box by, Field to);

    // A movable object gets pushed by a worker, to a given field
    public abstract void pushed(Worker by, Field to);

    public abstract String getMoveableString();
}
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Consol_Tester
{
    public abstract class Moveable
    {
        // Stores the given Field for the Moveable object as an attribute called "underThis"
        public abstract void SetField(Field field);

        // Destroys the Moveable object
        public abstract void Destroy();

        // Visitor pattern function
        public abstract void Accept(MoveableVisitor v);

        // A Moveable object Gets pushed by a box, to a given field
        public abstract void Pushed(Box by, Field to);

        // A Moveable object Gets pushed by a worker, to a given field
        public abstract void Pushed(Worker by, Field to);

        // Generates a string form of the class
        public abstract string GetMoveableString();
    }
}

/*
public abstract class Moveable {
    // Stores the given Field for the Moveable object as an attribute called "underThis"
    public abstract void setField(Field field);

    // Destroys the Moveable object
    public abstract void destroy();

    // Visitor pattern function
    public abstract void Accept(MoveableVisitor v);

    // A Moveable object Gets pushed by a box, to a given field
    public abstract void pushed(Box by, Field to);

    // A Moveable object Gets pushed by a worker, to a given field
    public abstract void pushed(Worker by, Field to);

    public abstract string GetMoveableString();
}
*/

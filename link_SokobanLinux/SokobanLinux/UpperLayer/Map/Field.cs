using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Features;
using Sokoban_2._0_Console.Graphics.Base;
using Sokoban_2._0_Console.Moveables;
using System;
using System.Collections.Generic;

namespace Sokoban_2._0_Console.UpperLayer.Map
{

    public class Field : MoveableVisitor
	{
		//The informations possibly seen by the user
        public class RepresentableData
        {
			public Field Self=null;
			public FieldContainer Container=null;
			public Moveable OnThis= null;
			public Feature Functionality= null;
			public bool IsIsolated = false;
			public int Friction = (Liquid.Honey.GetHashCode() + Liquid.Oil.GetHashCode()) / 2;
			public object modifiedSync = new object();
			public bool modified = true;
		}
		//Temporary data for the visitor pattern. TODO:eliminate visitor pattern
        public class TemporaryData
		{
			public int remainedStrength;
			public Direction enter;
        }
        //Most important data
		private static bool initFinished=false;
		private RepresentableData rep = new RepresentableData();
		private TemporaryData temp = new TemporaryData();
        private static FieldContainer all;
        public readonly FieldRepresentation representation;
		public static FieldContainer FieldContainer { set => all = value; }

		public int Friction
		{
			set{
				if (rep.Friction != value)
                    {
                        rep.Friction =value;
                        lock (rep.modifiedSync)
                            rep.modified = true;
                    }
                }
		}
		private Dictionary<Direction.dir, Position> neighbours = new Dictionary<Direction.dir, Position>();
		public Position this[Direction.dir d]{
			get
			{
				if (neighbours.ContainsKey(d))
					return neighbours[d];
				return null;
			}
		}
		public readonly Position position;
		public int LineCoord => position.Line;
		public int ColumnCoord=> position.Column;
		public bool IsIsolated => rep.IsIsolated;
		public bool IsEmpty => (rep.OnThis==null);

		//modification detection
        public bool Modified {
            get
            {
				lock (rep.modifiedSync)
                    return rep.modified;
            }
        }

        // Constructors for the fields
        public Field(Position _position, FieldRepresentation _representation)
        {
			initFinished = false;
			position = _position;
			rep.Container = all;
			rep.Self = this;
			_representation.Data = rep;
            representation = _representation;
        }

        // Adds the given field to its neighbours, in the given direction
		public void AddNeighbour(Position position, Direction direction, bool forced)
		{
			if (initFinished || (!forced && neighbours.ContainsKey(direction.value)))
                return;

			neighbours.Add(direction.value, position);
            lock (rep.modifiedSync)
                rep.modified = true;         
		}

        // Adds the given field to its neighbours, in the given direction
        public void RemoveNeighbour(Direction direction)
        {
			if (initFinished || !neighbours.ContainsKey(direction.value))
                return;

			neighbours.Remove(direction.value);
            lock (rep.modifiedSync)
                rep.modified = true;
        }

        // Adds a feature to the field
		public void AddFeature(Feature _functionality)
        {
			if (rep.Functionality == null)
            {
				rep.Functionality = _functionality;
                lock (rep.modifiedSync)
                    rep.modified = true;
            }
        }

        // Adds a Moveable object to the field
        public void AddMoveable(Moveable Moveable)
        {
			if (rep.OnThis == null)
            {
				rep.OnThis = Moveable;
                Moveable.SetField(this);
				if (rep.Functionality != null)
					rep.Functionality.Interact(Moveable);
                lock (rep.modifiedSync)
                    rep.modified = true;
            }
        }

        // The worker calls for the LeaveRequest function when it is Moved by the player
        // If the field has a neighbor in the given direction and that field is not Isolated,
        // the worker Gets Pushed
        public void LeaveRequest(Direction D, Worker w)
		{
            lock (rep.modifiedSync)
                rep.modified = true;
			if (!neighbours.ContainsKey(D.value))         
				return;

			if (!all[neighbours[D.value]].IsIsolated)
            {
				all[neighbours[D.value]].Push(D, w, w.strength);
				if (rep.OnThis == null)
                    lock (rep.modifiedSync)
                        rep.modified = true;
            }
        }

        // A box is being Pushed to this field
        // If this field has a Moveable object on it, the object on this field Gets Pushed to the next field, in the same direction
        // If not, the object given as a parameter will be set onto this field and Gets reMoved from its previous field
        /*public void Push(Direction direction, Box box, double remStrength)
        {
            if (onThis != null)
            {
                if (neighbours.ContainsKey(direction))
                    onThis.Pushed(box, neighbours[direction]);
                else
                    onThis.Pushed(box, null);


                if (onThis != null && ((remStrength - friction) > 0))
                {
                    enter = direction;
                    remainedStrength = remStrength - friction;
                    onThis.Accept(this);
                }
            }
            if (onThis == null)
            {
                onThis = box;
                box.SetField(this);
                neighbours[GetReverse()].RemoveMoveable();

                if (feature != null)
                {
                    feature.Interact(box);
                }
            }
        }*/

        // A worker is being Pushed to this field
        // If this field has a Moveable object on it, the worker Gets destroyed
        // After the object given as a parameter will be set onto this field and Gets reMoved from its previous field
        public void Push(Direction direction, Worker worker, int remStrength)
        {
			if (rep.OnThis != null)
            {
				rep.OnThis.Pushed(worker, null);

				if (rep.OnThis != null && ((remStrength - rep.Friction) > 0))
                {
                    temp.enter = direction;
					temp.remainedStrength = remStrength - rep.Friction;
					rep.OnThis.Accept(this);
                }
            }

			if (rep.OnThis == null)
            {
				rep.OnThis = worker;
                worker.SetField(this);
				all[neighbours[direction.GetReverse().value]].RemoveMoveable();
                lock (rep.modifiedSync)
                    rep.modified = true;

				if (rep.Functionality != null)
                {
					rep.Functionality.Interact(worker);
                }
            }
        }

        // ReMoves the Moveable object that is located on the field
        public void RemoveMoveable()
        {
			if (rep.OnThis != null)
            {
				rep.OnThis = null;
                lock (rep.modifiedSync)
                    rep.modified = true;
            }
        }

        // Destroys the Moveable object that is located on this field
        public void DestroyMoveable()
        {
			if (rep.OnThis != null)
            {
				rep.OnThis.Destroy();
            }
        }

        // Isolating the field from its neighbours by changing the isIsolated attribute
        public void Isolate()
        {
			if (rep.IsIsolated != true)
            {
				rep.IsIsolated = true;
                lock (rep.modifiedSync)
                    rep.modified = true;
            }
        }

		public static void FinishInit(){
			initFinished = true;
		}

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a box
        // If this field has a neighbor in the previously stored (enter) direction and that neighbor is not Isolated,
        // the box Gets Pushed to the next field
        /*public void Visit(Box b)
        {
            if (neighbours.ContainsKey(enter) && !neighbours[enter].GetIsolated())
            {
                neighbours[enter].Push(enter, b, remainedStrength);
            }
        }*/

        // Visitor pattern core, Gets the given Moveable type as argument
        // In this case, the given object is a worker
        // If this field has a neighbor in the previously stored (enter) direction and that neighbor is not Isolated and it's empty
        // the worker Gets Pushed to the next field
        public void Visit(Worker w)
        {
			Field neighbour = all[neighbours[temp.enter.value]];
			if (neighbours.ContainsKey(temp.enter.value) && !neighbour.IsIsolated && neighbour.IsEmpty)
            {
				neighbour.Push(temp.enter, w, temp.remainedStrength);
            }
        }
    }
}

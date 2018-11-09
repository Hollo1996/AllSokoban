using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    public class ControlInterface
    {
        protected Worker worker;
        public readonly Color id;
        private string UP, RIGHT, DOWN, LEFT, PUT_LIQUID;
        public string this[Direction.dir name]{
            get{
                switch (name)
                {
                    case Direction.dir.UP: return UP;
                    case Direction.dir.RIGHT: return RIGHT;
                    case Direction.dir.DOWN: return DOWN;
                    case Direction.dir.LEFT: return LEFT;
                    default: return PUT_LIQUID;
                }

            }
        }

        // Constructor for the control interface
        public ControlInterface(Color color, Worker w, string UP, string RIGHT, string DOWN, string LEFT, string PUT_LIQUID)
        {
            this.id = color;
            this.worker = w;
            w.SetOwner(this);

            this.UP = UP;
            this.RIGHT = RIGHT;
            this.DOWN = DOWN;
            this.LEFT = LEFT;
            this.PUT_LIQUID = PUT_LIQUID;
        }

        public string GetUP()
        {
            return UP;
        }

        public string GetRIGHT()
        {
            return RIGHT;
        }

        public string GetDOWN()
        {
            return DOWN;
        }

        public string GetLEFT()
        {
            return LEFT;
        }

        public string GetPUT_LIQUID()
        {
            return PUT_LIQUID;
        }

        // The players Moves his worker in a given direction
        public void Move(Direction d)
        {
            worker.Move(d);
        }

        // ReMoves the worker from the control interface
        // and signals this action to the game field
        public void RemoveWorker()
        {
            worker = null;
            GameField.GetInstance().OutOfWorkers();
        }

        // Return weather the control interface has a worker or not
        public bool hasWorker()
        {
            return worker != null;
        }

        // Called by GameField on keypress e
        public virtual void OnEvent(Event e)
        {
            string keyPressed = e.GetKeyPressed();

            if (keyPressed.Equals(UP))
            {
                this.worker.Move(Direction.UP);
            }
            if (keyPressed.Equals(RIGHT))
            {
                this.worker.Move(Direction.RIGHT);
            }
            if (keyPressed.Equals(DOWN))
            {
                this.worker.Move(Direction.DOWN);
            }
            if (keyPressed.Equals(LEFT))
            {
                this.worker.Move(Direction.LEFT);
            }
            if (keyPressed.Equals(PUT_LIQUID))
            {
                this.worker.AddLiquid(e.GetLiquid());
            }
        }

        public void listWorker(int point)
        {
            if (worker == null)
            {
                return;
            }

            worker.Print(point);
        }
    }
}

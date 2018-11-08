using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Moveables;
using Sokoban_2._0_Console.UpperLayer;
using Sokoban_2._0_Console.UpperLayer.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban_2._0_Console.Controls
{
    public class ControlInterface
    {
        //The Name Of the Player Seted through the Game manu
        private readonly string name;
        public  string Name {
            get { return name; }
        }
        //The Keys the Control Reacts for
        private Dictionary<ControlKeySetting.ControlEvent, int> eventKeyMap;
        //The Entity Controled by the player
        private Worker currentWorker;

        //Kontruktor sets all the parametres
        public ControlInterface(string _name,ControlKeySetting _eventKeyMap) {
            name = _name;
            eventKeyMap = _eventKeyMap.eventKeyMap;
        }
        public void AddWorker(Worker _currentWorker)
        {
            currentWorker = _currentWorker;
        }

        //We Handle the key presses
        public bool keyHandler(Object sender, int key) {
            ControlKeySetting.ControlEvent triggeredCE=ControlKeySetting.ControlEvent.None;
            foreach(ControlKeySetting.ControlEvent ce in eventKeyMap.Keys) {
                if (eventKeyMap[ce] == key)
                    triggeredCE = ce;
            }
            switch (triggeredCE)
            {
                case ControlKeySetting.ControlEvent.None: return false;
                case ControlKeySetting.ControlEvent.Up:
					if(currentWorker!=null)
                        currentWorker.Move(Direction.UP);
                    break;
				case ControlKeySetting.ControlEvent.Down:
                    if (currentWorker != null)
                        currentWorker.Move(Direction.DOWN);
                    break;
				case ControlKeySetting.ControlEvent.Right:
                    if (currentWorker != null)
                        currentWorker.Move(Direction.RIGHT);
                    break;
				case ControlKeySetting.ControlEvent.Left:
                    if (currentWorker != null)
                        currentWorker.Move(Direction.LEFT);
                    break;
				case ControlKeySetting.ControlEvent.PutHoney:
                    if (currentWorker != null)
                        currentWorker.AddLiquid(Liquid.Honey);
                    break;
				case ControlKeySetting.ControlEvent.PutOil:
                    if (currentWorker != null)
                        currentWorker.AddLiquid(Liquid.Oil);
                    break;
                default:return false;
            }
            return true;
        }

        // ReMoves the worker from the control interface
        // and signals this action to the game field
        public void RemoveWorker()
        {
            currentWorker = null;
            GameField.Instance.OutOfWorkers();
        }

        // Return weather the control interface has a worker or not
        public bool hasWorker()
        {
            return currentWorker != null;
        }

        public void listWorker(int point)
        {
            if (currentWorker == null)
            {
                return;
            }

            currentWorker.Print(point);
        }

        public void lost()
        {
        }

        public void won()
        {
        }
    }
}

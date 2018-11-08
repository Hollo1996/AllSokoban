using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sokoban_2._0_Console.Control
{
    public class ControlKeySetting
    {
        public enum ControlEvent : byte
        {
            Up = 0x0001,
            Down = 0x0002,
            Right = 0x0004,
            Left = 0x0008,
            PutHoney = 0x0010,
            PutOil = 0x0020
        }

        public Dictionary<ControlEvent, Keys> keyEventMap = new Dictionary<ControlEvent, Keys>();

        public ControlKeySetting()
        {
            keyEventMap.Add(ControlEvent.Up, Keys.Up);
            keyEventMap.Add(ControlEvent.Down, Keys.Down);
            keyEventMap.Add(ControlEvent.Right, Keys.Right);
            keyEventMap.Add(ControlEvent.Left, Keys.Left);
            keyEventMap.Add(ControlEvent.PutHoney, Keys.NumPad1);
            keyEventMap.Add(ControlEvent.PutOil, Keys.NumPad2);
        }
        public ControlKeySetting(Dictionary<ControlEvent, Keys> _keyEventMap)
        {
            keyEventMap.Add(ControlEvent.Up, Keys.Up);
            keyEventMap.Add(ControlEvent.Down, Keys.Down);
            keyEventMap.Add(ControlEvent.Right, Keys.Right);
            keyEventMap.Add(ControlEvent.Left, Keys.Left);
            keyEventMap.Add(ControlEvent.PutHoney, Keys.NumPad1);
            keyEventMap.Add(ControlEvent.PutOil, Keys.NumPad2);
            foreach (ControlEvent ce in _keyEventMap.Keys)
                if (!keyEventMap.ContainsKey(ce))
                    keyEventMap.Add(ce, _keyEventMap[ce]);
        }
    }
}

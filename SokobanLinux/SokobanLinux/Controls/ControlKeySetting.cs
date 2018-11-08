using System.Collections.Generic;
using System.Windows.Forms;

namespace Sokoban_2._0_Console.Controls
{
    public class ControlKeySetting
    {
        public enum ControlEvent : byte
        {
            None =0x0000,
            Up = 0x0001,
            Down = 0x0002,
            Right = 0x0004,
            Left = 0x0008,
            PutHoney = 0x0010,
            PutOil = 0x0020
        }
        
        public Dictionary<ControlEvent, int> eventKeyMap = new Dictionary<ControlEvent, int>();

        public ControlKeySetting()
        {
            eventKeyMap.Add(ControlEvent.Up, Keys.W.GetHashCode());
            eventKeyMap.Add(ControlEvent.Down, Keys.S.GetHashCode());
            eventKeyMap.Add(ControlEvent.Right, Keys.D.GetHashCode());
            eventKeyMap.Add(ControlEvent.Left, Keys.A.GetHashCode());
            eventKeyMap.Add(ControlEvent.PutHoney, Keys.H.GetHashCode());
            eventKeyMap.Add(ControlEvent.PutOil, Keys.O.GetHashCode());
        }
        public ControlKeySetting(Dictionary<ControlEvent, int> _keyEventMap)
        {
            eventKeyMap.Add(ControlEvent.Up, Keys.W.GetHashCode());
            eventKeyMap.Add(ControlEvent.Down, Keys.D.GetHashCode());
            eventKeyMap.Add(ControlEvent.Right, Keys.F.GetHashCode());
            eventKeyMap.Add(ControlEvent.Left, Keys.A.GetHashCode());
            eventKeyMap.Add(ControlEvent.PutHoney, Keys.H.GetHashCode());
            eventKeyMap.Add(ControlEvent.PutOil, Keys.O.GetHashCode());

            foreach (ControlEvent ce in _keyEventMap.Keys)
                if (_keyEventMap.ContainsKey(ce))
                    eventKeyMap.Add(ce, _keyEventMap[ce]);
        }
    }
}

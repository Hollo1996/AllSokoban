using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Control
{
    class Control
    {
        private string name;
        private ControlKeySetting eventKeyMap;

        Control(string _name,ControlKeySetting _eventKeyMap) {
            name = _name;
            eventKeyMap = _eventKeyMap;
        }
    }
}

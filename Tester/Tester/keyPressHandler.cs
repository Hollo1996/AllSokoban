using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    class keyPressHandler
    {
        public static int idCounter = 0;
        public int id;
        public keyPressHandler() {
            id = idCounter;
            idCounter++;
        }
        public void onKeyPress(Object sender, ConsoleKeyInfo keyInfo) {
            Console.WriteLine(id.ToString()+" "+ keyInfo.KeyChar);
        }
    }
}

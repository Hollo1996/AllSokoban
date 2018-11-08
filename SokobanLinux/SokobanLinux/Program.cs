using Sokoban_2._0_Console.UpperLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Instance.CommandLoop();
        }
    }
}

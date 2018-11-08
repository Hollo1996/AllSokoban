using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Consol_Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            GameField gf = GameField.GetInstance();
            gf.LoadMap("test_map2");
            gf.listFeatures("bu");
            gf.listFeatures("ft");
            gf.listFeatures("ho");
            gf.listFeatures("bc");
            gf.listFields();
            gf.listFieldFieldConnectivities();
            gf.listWorkers();
            gf.listBoxes();
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    class Program
    {
        //Key Converting Laboratori
        /*static void Main(string[] args)
        {
            if (((ConsoleKey.Subtract).GetHashCode())==((Keys.Subtract).GetHashCode()))
                Console.WriteLine("Easyyy!!"+ ((ConsoleKey.Subtract).GetHashCode()).ToString()+","+((int)(ConsoleKey.Subtract)));
            Console.ReadKey();
        }*/

        //Threading and key handleing laboratory
        static ConsoleKeyInfo ckiGlobal;
        static object sync = new object();
        static byte read = 4;
        static AutoResetEvent are = new AutoResetEvent(false);

        static Thread t0 = new Thread(new ThreadStart(readKey));
        static Thread t1 = new Thread(new ParameterizedThreadStart(throwKey));
        static Thread t2 = new Thread(new ParameterizedThreadStart(throwKey));
        static Thread t3 = new Thread(new ParameterizedThreadStart(throwKey));
        static Thread t4 = new Thread(new ParameterizedThreadStart(throwKey));

        static void Main(string[] args)
        {
            t0.Start();
            t1.Start(0);
            t2.Start(1);
            t3.Start(2);
            t4.Start(3);
        }

        private static void readKey()
        {
            while (true)
            {
                lock (sync)
                {
                    if (read == 4)
                    {
                        ckiGlobal = Console.ReadKey();
                        Console.Clear();
                        read = 0;
                        are.Set();
                        are.Set();
                        are.Set();
                        are.Set();
                        if (ckiGlobal.Key == ConsoleKey.Escape)
                            break;
                    }
                }
            }
        }

        private static void throwKey(object param)
        {
            int flagindex = (int)param;
            keyPressHandler kph = new keyPressHandler();
            while (true)
            {
                are.WaitOne();
                lock (sync)
                    if (read == 4)
                        are.WaitOne();
                ConsoleKeyInfo cki = ckiGlobal;
                if (ckiGlobal.Key == ConsoleKey.Escape)
                    return;
                lock (sync)
                    read++;
                kph.onKeyPress(new object(), cki);
            }
        }
    }
}

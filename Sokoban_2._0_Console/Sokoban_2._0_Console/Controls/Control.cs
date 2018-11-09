using Sokoban_2._0_Console.Graphics;
using Sokoban_2._0_Console.Moveables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Controls
{
    //AutoResetEvent
    public class Control
    {
        //Main data objects
        private int escape;
        private int pressedKeyCode;
        private Thread[] threads;
        private ControlKeySetting[] controlKeySettings=new ControlKeySetting[1];
        private ControlInterface[] controlInterfaces;
        public ControlInterface this[int index] => controlInterfaces[index % controlInterfaces.Length];
        public ControlInterface[] ControlInterfaces=> controlInterfaces;
        //Sincronizatioon objects
        private AutoResetEvent are = new AutoResetEvent(false);
        private object semaforSetSyncObject = new object();
        private byte semafor = 1;
        private bool loopRunning = false;
        
        //This is the only option to change the gam multiplayer. Not importent yet.
        public void setPlayerCount(byte controlInterfaceCount)
        {
            lock (semaforSetSyncObject)
            {
                semafor = controlInterfaceCount;
                if (controlInterfaceCount == controlInterfaces.Length)
                    return;
                controlInterfaces = new ControlInterface[controlInterfaceCount];
                threads = new Thread[controlInterfaceCount];
                for (int i = 0; i < controlInterfaceCount; i++)
                {
                    controlInterfaces[i] = new ControlInterface("player" + (i + 1).ToString(), new ControlKeySetting());
                    threads[i] = new Thread(new ParameterizedThreadStart(transferKeyLoop));
                }
            }
        }

        //Constructor sets the game for a singleplayer game
        //SingletonPattern
        private Control()
        {
            escape = ConsoleKey.Escape.GetHashCode();
            controlInterfaces = new ControlInterface[1];
            threads = new Thread[1];
            controlInterfaces[0] = new ControlInterface("player1", new ControlKeySetting());
            threads[0] = new Thread(new ParameterizedThreadStart(transferKeyLoop));

        }
        private static Control onlyInstance = new Control();
        public static Control Instance => onlyInstance;

        //Reads keys from Console, Clears the Console and starts and syncronyses key code transport. 
        public void readKeyLoop()
        {
            lock (semaforSetSyncObject) {
                if (loopRunning)
                    return;
                else
                    loopRunning = true;
            }
            for (int i = 0; i < threads.Length; i++)
                threads[i].Start(i);
            while (true)
            {
                lock (semaforSetSyncObject)
                {
                    if (semafor == controlInterfaces.Length)
                    {
                        pressedKeyCode = Console.ReadKey().Key.GetHashCode();
                        semafor = 0;
                        for (int i = 0; i < controlInterfaces.Length; i++)
                            are.Set();
                        if (pressedKeyCode == escape)
                        {
                            loopRunning = false;
                            break;
                        }
                    }
                }
            }
        }
        //Transports key code to one of the ControlInterfaces.
        private void transferKeyLoop(object param)
        {
            ControlInterface target = controlInterfaces[(int)param];
            while (true)
            {
                are.WaitOne();
                int keyCode = pressedKeyCode;
                if (pressedKeyCode == escape)
                {
                    break;
                }
                bool found=target.keyHandler(this, pressedKeyCode);
                lock (semaforSetSyncObject) {
                    semafor++;
                    if (semafor == controlInterfaces.Length&& found == false)
                    {
                        Console.WriteLine("Invalidate transferKeyLoop");
                        Graphic.Instance.Invalidate();
                    }
                }
            }
        }
    }
}

using Sokoban_2._0_Console.UpperLayer.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console.Graphics
{
    public class Graphic
    {
        private AutoResetEvent are = new AutoResetEvent(false);
        private object validSync = new object();
        private object pictureSync = new object();
        private object mapSync = new object();
        private object runningSync = new object();

        private bool valid = false;
        private bool running = false;
        private char[,] Picture;
        private Dictionary<int, Dictionary<int, Field>> map =new Dictionary<int, Dictionary<int, Field>>();
        public void SetPictureSize(int height,int width)
        {
            Picture = new char[height, width];
            Console.WriteLine("Invalidate SetPictureSize");
            Invalidate();
        }
        public void SetMap(Dictionary<int, Dictionary<int, Field>> _map)
        {
            map = _map;
            Console.WriteLine("Invalidate SetMap");
            Invalidate();
        }
        private void drawOnPicture(char[,] drawing, int lineCoord,int columnCoord)
        {
            if (((drawing.GetLength(0) + lineCoord) > Picture.GetLength(0))||
                ((drawing.GetLength(1) + columnCoord) > Picture.GetLength(1)))
                return;
            for (int line = 0; line < drawing.GetLength(0); line++)
            {

                for (int column = 0; column < drawing.GetLength(1); column++)
                {
                    Picture[lineCoord + line,columnCoord + column]=drawing[line,column];
                }
            }
        }

        Thread updater;
        Thread painter;
        private Graphic() {
            updater = new Thread(new ThreadStart(UpdaterLoop));
            painter = new Thread(new ThreadStart(PaintLoop));
        }
        private static Graphic onlyInstance = new Graphic();
        public static Graphic Instance => onlyInstance;




        public void Invalidate() {
            lock(validSync)
                valid = false;
        }
        public void UpdaterLoop() {
            if (running)
                return;
            bool oldvalid=true;
            while (true)
            {
                lock (pictureSync)
                {
                    lock (mapSync)
                    {
                        foreach (int mapLine in map.Keys)
                        {
                            foreach(int mapColumn in map[mapLine].Keys)
                            {
                                if (map[mapLine][mapColumn].Modified)
                                {
                                    drawOnPicture(((LittleConsole.LittleRepresentation)(map[mapLine][mapColumn].representation)).Representation, mapLine * 3, mapColumn * 5);
                                }
                            }
                        }
                    }
                }
                lock (validSync)
                {
                    if (!valid && oldvalid != valid)
                    {
                        //Console.Clear();
                        are.Set();
                    }
                    oldvalid = valid;
                }
            }
        }

        public void Start()
        {
            updater.Start();
            painter.Start();
        }

        private void PaintLoop()
        {
            int x = 0;
            while (true)
            {
                are.WaitOne();
                lock (validSync)
                    valid = true;
            lock (pictureSync)
                {
                    if (Picture != null)
                    {
                        Console.Clear();
                        Console.WriteLine(x.ToString());
                        x++;
                        for (int i = 0; i < Picture.GetLength(0); i++)
                        {
                            for (int j = 0; j < Picture.GetLength(1); j++)
                            {
                                Console.Write(Picture[i,j]);
                            }
                            Console.Write('\n');
                        }
                        Console.WriteLine("Paint Ended!");
                    }
                }
            }
        }

    }
}

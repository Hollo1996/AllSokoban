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
        private AutoResetEvent paintAre = new AutoResetEvent(false);
        private static AutoResetEvent updateAre = new AutoResetEvent(false);
        private static object updateSync = new object();
        private object pictureSync = new object();
        private object mapSync = new object();
        private object runningSync = new object();

        private bool running = false;
		private char[,] Picture;
		private static FieldContainer mapContainer;
        public static FieldContainer FieldContainer
        {
            set
            {
                mapContainer = value;
                Console.WriteLine("Invalidate SetMap");
                Invalidate();
            }
        }

        private Thread updater;
        private Thread painter;

        private Graphic()
        {
            updater = new Thread(new ThreadStart(UpdaterLoop));
            painter = new Thread(new ThreadStart(PaintLoop));
        }
        private static Graphic onlyInstance = new Graphic();
        public static Graphic Instance => onlyInstance;


        public void Start()
		{
			updateAre.Reset();
			paintAre.Reset();
            updater.Start();
            painter.Start();
        }
        public void Stop()
		{
			updateAre.WaitOne();
            paintAre.WaitOne();
        }

		public Position pictureSize
		{         
		set{
				Picture = new char[value.Line, value.Column];
				Console.WriteLine("Invalidate SetPictureSize");
				Invalidate();
			}
		}

        private void drawOnPicture(char[,] drawing, int lineCoord, int columnCoord)
        {
            if (((drawing.GetLength(0) + lineCoord) > Picture.GetLength(0)) ||
                ((drawing.GetLength(1) + columnCoord) > Picture.GetLength(1)))
                return;
            for (int line = 0; line < drawing.GetLength(0); line++)
            {

                for (int column = 0; column < drawing.GetLength(1); column++)
                {
                    Picture[lineCoord + line, columnCoord + column] = drawing[line, column];
                }
            }
        }        



        public static void Invalidate()
        {
			updateAre.Set();
        }
        private void UpdaterLoop()
        {
            lock (runningSync)
                if (running)
                    return;
                else
                    running = true;
			bool changed = false;
			lock (pictureSync)
				for (int i = 0; i < Picture.GetLength(0); i++)
					for (int j = 0; j < Picture.GetLength(1); j++)
						Picture[i, j] = '*';
			while (true)
            {
                lock (pictureSync)
                {
                    lock (mapSync)
                    {
						foreach (int mapColumn in mapContainer.fieldsMap.Keys)
                        {
							foreach (int mapLine in mapContainer.fieldsMap[mapColumn].Keys)
                            {
								if (mapContainer.fieldsMap[mapColumn][mapLine].Modified)
                                {
									changed = true;
									drawOnPicture(((LittleConsole.LittleRepresentation)(mapContainer.fieldsMap[mapColumn][mapLine].representation)).Representation, mapLine * 3, mapColumn * 5);
                                }
                            }
                        }
                    }
				}
				if (changed)
				{
                    paintAre.Set();
					updateAre.WaitOne();
				}
				changed = false;
            }
        }
        private void PaintLoop()
        {
            int x = 0;
            bool unrelevant;
            bool start = true;
            while (true)
            {
                paintAre.WaitOne();
                lock (pictureSync)
                {
                    if (Picture != null)
                    {
                        x++;
                        Console.SetCursorPosition(0, 0);
						unrelevant = false;
                        if (start)
							Console.Clear();
                        for (int i = 0; i < Picture.GetLength(0); i++)
						{
                            Console.SetCursorPosition(0, i);
                            for (int j = 0; j < Picture.GetLength(1); j++)
                            {
                                if (!unrelevant && Picture[i, j] == '*')
                                {
                                    unrelevant = true;
                                    continue;
                                }
                                else if (Picture[i, j] != '*' && unrelevant)
                                {
                                    unrelevant = false;
                                    Console.SetCursorPosition(j, i);
                                }
                                if (unrelevant)
                                {
                                    continue;
                                }
                                else
                                {
                                    Console.Write(Picture[i, j]);
                                    Picture[i, j] = '*';
                                }
                            }

                            if (start)
                                Console.Write('\n');
                        }
                        if (start)
                            start = false;
						Console.SetCursorPosition(0, Picture.GetLength(0));
                        Console.WriteLine(x.ToString());
                        Console.WriteLine("Paint Ended!");
                    }
                }
            }
        }

    }
}

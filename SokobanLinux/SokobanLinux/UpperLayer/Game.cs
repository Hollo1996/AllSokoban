using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sokoban_2._0_Console.Controls;
using Sokoban_2._0_Console.Graphics;
using Sokoban_2._0_Console.Graphics.Base;
using Sokoban_2._0_Console.Graphics.LittleConsole;
using Sokoban_2._0_Console.UpperLayer.Map;

namespace Sokoban_2._0_Console.UpperLayer
{
    //Game's responsibility is to:
    //-Inicialise all the highest level components with their thread, and connect them together.
    //-Handle all the game set optiones the User Gives before starting a game
    //-Start the game
    //-Handle the final results
    public class Game
    {
        private bool matchRunning = false;
		bool Running => matchRunning;
		public static readonly DataRepresentationFactory viewFactory = new LittleConsoleDataRepresentationFactory();

        //sets default settings
        //Loads in list of Saved games
        //Calls Options loop
        //private becouse of singleton pattern
		private  Game() {  }
        //Singleton Pattern
        private static readonly Game onlyInstance = new Game();
		public static Game Instance => onlyInstance;

        //Loads Command from a txt
        public void LoadCommand(string nameOfTxt) {

            using (StreamReader file = new StreamReader(nameOfTxt + @".txt"))
            {
                while (!file.EndOfStream) {
                    CommandHandler(file.ReadLine());
                }
            }

        }
        //Reads Commands from the standard input
        public void CommandLoop() {
            string command="";
			while (string.Compare(command, "Close", StringComparison.Ordinal) != 0) {
				if (string.Compare(command, "", StringComparison.Ordinal) != 0)
                    CommandHandler(command);
                command=Console.ReadLine();
            }
        }

        //Handles all command loaded from TXT
        public bool CommandHandler(string command) {
            string[] commandAndParams= command.Split();
            switch (commandAndParams[0])
            {
                case "Start:":Start();
                    break;
                case "Stop:":Stop();
                    break;
                case "Load:":Load();
                    break;
                case "Save:":Save();
                    break;
                default: return false;
            }
            return true;
        }
        
        //Starts a whole new game
        public bool Start() {
            if(matchRunning)
				return !matchRunning;
            FieldContainer.Send();
			GameField.Instance.LoadMap("new/Neighbourhood/test_map9");
            GameField.Instance.AddControlInterface(Control.Instance.ControlInterfaces);
            Graphic.Instance.Start();
            Control.Instance.readKeyLoop();
			matchRunning = true;
            return true;
        }

        //Stops the current game
        public bool Stop()
        {
            return matchRunning;
            //TODO
        }
        //Loads Saved }
        public bool Load()
        {
            return !matchRunning;
            //TODO
        }
        //Saves current Game
        public bool Save()
        {
            return matchRunning;
            //TODO
        }
    }
}

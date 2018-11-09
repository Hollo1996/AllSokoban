using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    //Game's responsibility is to:
    //-Inicialise all the highest level components with their thread, and connect them together.
    //-Handle all the game set optiones the User Gives before starting a game
    //-Start the game
    //-Handle the final results
    public class newGame
    {
        bool matchRunning = false;

        //sets default settings
        //Loads in list of Saved games
        //Calls Options loop
        //private becouse of singleton pattern
        private newGame() {
            //TODO
        }
        //Singleton Pattern
        private static newGame onlyInstance = new newGame();
        public static newGame Instance { get => onlyInstance; }

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
            while (command.CompareTo("Close")!=0) {
                if (command != "")
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
            return !matchRunning;
            //TODO 
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

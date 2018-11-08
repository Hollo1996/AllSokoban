using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Console_Tester
{
    public class Game
    {
        private List<Player> players = new List<Player>();

        private Game() { }
        static Game onlyInstance = new Game();
        public static Game GetInstance()
        {
            return onlyInstance;
        }

        internal void EndGame()
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerByColor(string color)
        {
            Player player = null;
            foreach (Player p in players)
            {
                if (p.GetCI().id.Equals(Color.FromName(color)))
                {
                    player = p;
                }
            }

            return player;
        }


        // Ends the game, signals every player whetPrinther they won or lost
        public void endGame()
        {
            Player winner = null;

            for (int i = 0; i < players.Count(); i++)
            {
                int place = GameField.GetInstance().GetPlace(players[i].GetCI().id);
                if (place == 1)
                {
                    players[i].won();
                    winner = players[i];
                }
                else
                {
                    players[i].lost();
                }
            }
            // shall iterate through the colors and Getting each color's place
            // htf do we reach all colors?

            if (winner != null)
                Console.WriteLine("winner:" + winner.GetName());
        }

        //creates new player and Adds it to players
        public void createPlayer(string name, string color, string UP, string RIGHT, string DOWN, string LEFT, string PUT_LIQUID)
        {
            Player player = new Player(name);
            player.AddControl(GameField.GetInstance()
                    .createControlInterface(
                            Color.FromName(color),
                            UP,
                            RIGHT,
                            DOWN,
                            LEFT,
                            PUT_LIQUID
                    )
            );

            players.Add(player);
        }
    }
}

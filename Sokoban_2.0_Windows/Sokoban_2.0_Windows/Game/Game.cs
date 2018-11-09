using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Windows
{
    class Game
    {
    }
}

/*package hu.bme.sch.sokoban.game;

import hu.bme.sch.sokoban.game.controls.ControlInterface;
import hu.bme.sch.sokoban.game.entities.Worker;
import hu.bme.sch.sokoban.game.graphics.SokobanView;
import hu.bme.sch.sokoban.game.map.GameField;

import java.awt.*;
import java.util.ArrayList;

//singleton
public class Game {
    private ArrayList<Player> players = new ArrayList<>();
    private SokobanView view;

    private static Game ourInstance = new Game();

    public static Game getInstance() {
        return ourInstance;
    }

    private Game() {

    }

    public Player getPlayerByColor(String color) {
        Player player = null;
        for (Player p : players) {
            if(p.getCI().getId().equals(getColorByString(color))) {
                player = p;
            }
        }

        return player;
    }

    public void setView(SokobanView view) {
        this.view = view;
    }

    private Color getColorByString(String color) {
        color = color.toLowerCase();

        switch (color) {
            case "black":
                return new Color(0, 0, 0);
            case "white":
                return new Color(255, 255, 255);
            case "grey":
                return new Color(100, 100, 100);
            case "red":
                return new Color(255, 0, 0);
            case "green":
                return new Color(0, 255, 0);
            case "blue":
                return new Color(0, 0, 255);
            case "orange":
                return new Color(255, 99, 71);
            case "yellow":
                return new Color(255, 255, 0);
            case "cyan":
                return new Color(0, 255, 255);
            case "magenta":
                return new Color(255, 0, 255);
            case "pink":
                return new Color(255, 192, 203);
            case "darkGrey":
                return new Color(50, 50, 50);
            default:
                return new Color(0, 0, 0);
        }
    }

    // Ends the game, signals every player whetprinther they won or lost
    public void endGame() {
        Player winner = null;

        for(int i=0;i<players.size();i++) {
       		int place=GameField.getInstance().getPlace(players.get(i).getCI().getId());
       		if(place==1) {
       			players.get(i).won();
       			winner = players.get(i);
       		}
       		else {
       			players.get(i).lost();
       		}
       	}
        // shall iterate through the colors and getting each color's place
        // htf do we reach all colors?

        assert winner != null;
        view.end(winner.getName());
    }

    //creates new player and adds it to players
    public void createPlayer(String name, String color, String UP, String RIGHT, String DOWN, String LEFT, String PUT_LIQUID) {
        Player player = new Player(name);
        player.addControl(GameField.getInstance()
                .createControlInterface(
                        getColorByString(color),
                        UP,
                        RIGHT,
                        DOWN,
                        LEFT,
                        PUT_LIQUID
                )
        );

        players.add(player);
    }
}
*/

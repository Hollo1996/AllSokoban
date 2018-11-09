package hu.bme.sch.sokoban;

import hu.bme.sch.sokoban.game.Game;
import hu.bme.sch.sokoban.game.graphics.SokobanView;

import java.util.Scanner;

public class Main {
	static SokobanView sokobanView;

    //Main event loop, keyboard events passed to GameField
    public static void main(String[] args) {
        Game game = Game.getInstance();

        sokobanView = new SokobanView();
//        startGameLoop();
    }
}

package hu.bme.sch.sokoban.game.graphics;

import hu.bme.sch.sokoban.Event;
import hu.bme.sch.sokoban.game.Game;
import hu.bme.sch.sokoban.game.Player;
import hu.bme.sch.sokoban.game.map.GameField;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;

class SokobanKeyListener implements KeyListener {
    SokobanView sokobanView;

    SokobanKeyListener(SokobanView v){
        sokobanView = v;
    }

    @Override
    public void keyTyped(KeyEvent e) {
    }

    @Override
    public void keyPressed(KeyEvent e) {
        if(e.getKeyCode() == KeyEvent.VK_ESCAPE) {
            Game.getInstance().endGame();
        }

        if(e.getKeyCode() == KeyEvent.VK_D){
            try {
                Player player = Game.getInstance().getPlayerByColor("blue");

                GameField.getInstance().onEvent(new Event(player.getCI().getRIGHT()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_A){
            try {
                Player player = Game.getInstance().getPlayerByColor("blue");

                GameField.getInstance().onEvent(new Event(player.getCI().getLEFT()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_S){
            try {
                Player player = Game.getInstance().getPlayerByColor("blue");

                GameField.getInstance().onEvent(new Event(player.getCI().getDOWN()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_W){
            try {
                Player player = Game.getInstance().getPlayerByColor("blue");

                GameField.getInstance().onEvent(new Event(player.getCI().getUP()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_Q) { // oil
            Player player = Game.getInstance().getPlayerByColor("blue");

            GameField.getInstance().onEvent(new Event(player.getCI().getPUT_LIQUID(), 0.5));
            sokobanView.refreshView();
        }
        if(e.getKeyCode() == KeyEvent.VK_E) { // honey
            Player player = Game.getInstance().getPlayerByColor("blue");

            GameField.getInstance().onEvent(new Event(player.getCI().getPUT_LIQUID(), 1.5));
            sokobanView.refreshView();
        }
        if(e.getKeyCode() == KeyEvent.VK_RIGHT){
            try {
                Player player = Game.getInstance().getPlayerByColor("red");

                GameField.getInstance().onEvent(new Event(player.getCI().getRIGHT()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_LEFT){
            try {
                Player player = Game.getInstance().getPlayerByColor("red");

                GameField.getInstance().onEvent(new Event(player.getCI().getLEFT()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_DOWN){
            try {
                Player player = Game.getInstance().getPlayerByColor("red");

                GameField.getInstance().onEvent(new Event(player.getCI().getDOWN()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_UP){
            try {
                Player player = Game.getInstance().getPlayerByColor("red");

                GameField.getInstance().onEvent(new Event(player.getCI().getUP()));
                sokobanView.refreshView();
            } catch (Exception e1) {
                e1.printStackTrace();
            }
        }
        if(e.getKeyCode() == KeyEvent.VK_CONTROL) { // oil
            Player player = Game.getInstance().getPlayerByColor("red");

            GameField.getInstance().onEvent(new Event(player.getCI().getPUT_LIQUID(), 0.5));
            sokobanView.refreshView();

        }
        if(e.getKeyCode() == KeyEvent.VK_SHIFT) { // honey
            Player player = Game.getInstance().getPlayerByColor("red");

            GameField.getInstance().onEvent(new Event(player.getCI().getPUT_LIQUID(), 1.5));
            sokobanView.refreshView();

        }
    }

    @Override
    public void keyReleased(KeyEvent e) {
    }
}
package hu.bme.sch.sokoban.game.graphics;

import hu.bme.sch.sokoban.game.Game;
import hu.bme.sch.sokoban.game.map.GameField;
import hu.bme.sch.sokoban.game.map.WrongCoordException;
import hu.bme.sch.sokoban.game.map.WrongDirectionFormat;
import hu.bme.sch.sokoban.game.map.WrongLineFormat;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

public class SokobanView extends JFrame {
    private static final int WINDOW_WIDTH = 480;
    private static final int WINDOW_HEIGHT = 320;

    private int gameWidth = 480;
    private int gameHeight = 320;

    private String mapName = "small_map";

    private JPanel panel;
    private Menu menu;
    private Selection selection;
    private GameOver gameOver;

    public SokobanView(){
        this.setResizable(false);
        this.setLayout(new BorderLayout());
        this.setVisible(true);
        this.setTitle("Killer Sokoban");
        
        Insets insets = this.getInsets();
        this.setSize(new Dimension(insets.left + insets.right + WINDOW_WIDTH,
                    insets.top + insets.bottom + WINDOW_HEIGHT));
        this.setVisible(true);
        this.setResizable(false);
        
        menu = new Menu(this);
        this.add(menu, BorderLayout.CENTER);
        this.setFocusable(true);
        this.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
        
        this.revalidate();
        this.repaint();
    }
    
    public void next(String mapName) {
        if(mapName != null) {
            this.mapName = mapName;
        }

        switch (mapName) {
            case "small_map":
                gameWidth = 480;
                gameHeight = 320;
                break;
            case "medium_map":
                gameWidth = 640;
                gameHeight = 480;
                break;
            case "large_map":
                gameWidth = 800;
                gameHeight = 640;
                break;
        }

    	selection = new Selection(this);

    	this.remove(menu);
    	this.add(selection , BorderLayout.CENTER);
    	
    	this.revalidate();
        this.repaint();
    }

    public void start(String name1, String name2) {
        try {
            Insets insets = this.getInsets();
            this.setSize(new Dimension(insets.left + insets.right + gameWidth,
                    insets.top + insets.bottom + gameHeight));
            this.setVisible(true);
            this.setResizable(false);

            Game.getInstance().setView(this);
            GameField.getInstance().loadMap(mapName);
            Game.getInstance().createPlayer(name1, "blue", "w", "d", "s", "a", "space");
            Game.getInstance().createPlayer(name2, "red", "up", "right", "down", "left", "ctrl");

            panel = new JPanel();
            panel.setLayout(null);
            panel.setVisible(true);
            panel.setSize(new Dimension(gameWidth, gameHeight));

            this.remove(selection);
            this.add(panel, BorderLayout.CENTER);
            this.drawGameField();
            this.addKeyListener(new SokobanKeyListener(this));

            this.revalidate();
            this.repaint();
        } catch (IOException | WrongCoordException | WrongLineFormat | WrongDirectionFormat e) {
            e.printStackTrace();
        }
    }
    
    public void end(String name) {
    	Insets insets = this.getInsets();
    	this.setSize(new Dimension(insets.left + insets.right + WINDOW_WIDTH,
                insets.top + insets.bottom + WINDOW_HEIGHT));
    	gameOver= new GameOver(name, this);
    	
    	panel.removeAll();
    	this.remove(panel);
    	this.add(gameOver, BorderLayout.CENTER);
    	
    	this.revalidate();
        this.repaint();
    }
    
    public void menu() {
    	this.remove(gameOver);
    	this.add(menu, BorderLayout.CENTER);
    	
    	this.revalidate();
        this.repaint();
    }

    public void refreshView(){
        if(panel != null) {
            panel.removeAll();
        }
        this.drawGameField();
        this.revalidate();
        this.repaint();
    }

    private void drawGameField(){
        GameField gameField = GameField.getInstance();
        try {
            gameField.drawFields(this);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void drawField(int x, int y, String horizontalTop, String horizontalBot, String verticalLeft,  String verticalRight, String middlee) throws IOException {
        BufferedImage corner 	= ImageIO.read(new File("imageSet" + File.separator + "corner.png"));
        BufferedImage hBlackT 	= ImageIO.read(new File(horizontalTop));
        BufferedImage hBlackB 	= ImageIO.read(new File(horizontalBot));
        BufferedImage vBlackL 	= ImageIO.read(new File(verticalLeft));
        BufferedImage vBlackR 	= ImageIO.read(new File(verticalRight));
        BufferedImage middle 	= ImageIO.read(new File(middlee));

        JLabel corner1Label 		= new JLabel(new ImageIcon(corner));
        JLabel corner2Label 		= new JLabel(new ImageIcon(corner));
        JLabel corner3Label 		= new JLabel(new ImageIcon(corner));
        JLabel corner4Label 		= new JLabel(new ImageIcon(corner));
        JLabel horizontalTopLabel 	= new JLabel(new ImageIcon(hBlackT));
        JLabel horizontalBotLabel 	= new JLabel(new ImageIcon(hBlackB));
        JLabel verticalLeftLabel 	= new JLabel(new ImageIcon(vBlackL));
        JLabel verticalRightLabel 	= new JLabel(new ImageIcon(vBlackR));
        JLabel middleLabel 			= new JLabel(new ImageIcon(middle));

        corner1Label.setSize(			new Dimension(2, 2));
        corner1Label.setLocation(		new Point(x	, y));
        corner2Label.setSize(			new Dimension(2, 2));
        corner2Label.setLocation(		new Point(x+30	, y));
        corner3Label.setSize(			new Dimension(2, 2));
        corner3Label.setLocation(		new Point(x	, y+30));
        corner4Label.setSize(			new Dimension(2, 2));
        corner4Label.setLocation(		new Point(x+30	, y+30));

        middleLabel.setSize(			new Dimension(28, 28));
        middleLabel.setLocation(		new Point(x+2, y+2));

        horizontalBotLabel.setSize(		new Dimension(28, 2));
        horizontalBotLabel.setLocation(	new Point(x+2, y+30));

        horizontalTopLabel.setSize(		new Dimension(28, 2));
        horizontalTopLabel.setLocation(	new Point(x+2, y));

        verticalLeftLabel.setSize(		new Dimension(2, 28));
        verticalLeftLabel.setLocation(	new Point(x, y+2));

        verticalRightLabel.setSize(		new Dimension(2, 28));
        verticalRightLabel.setLocation(	new Point(x+30, y+2));

        panel.add(corner1Label);
        panel.add(corner2Label);
        panel.add(corner3Label);
        panel.add(corner4Label);
        panel.add(middleLabel);
        panel.add(horizontalBotLabel);
        panel.add(horizontalTopLabel);
        panel.add(verticalLeftLabel);
        panel.add(verticalRightLabel);
    }
}

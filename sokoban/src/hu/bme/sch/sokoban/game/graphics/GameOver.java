package hu.bme.sch.sokoban.game.graphics;

import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Insets;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.JTextField;

public class GameOver extends JPanel{
	private Image background;
	private SokobanView parentView;
	
	public GameOver(String winner, SokobanView parentView){
		this.parentView = parentView;
		this.setLayout(new BoxLayout(this, BoxLayout.PAGE_AXIS));

        try {
            background= ImageIO.read(new File("imageSet" + File.separator + "menu.png"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        
        this.add(Box.createRigidArea(new Dimension(0,50)));
        
        JTextField textField1=new JTextField("The winner is:");
    	textField1.setAlignmentX(Component.CENTER_ALIGNMENT);
    	textField1.setMaximumSize(new Dimension(284, 31));
    	textField1.setBorder(BorderFactory.createLineBorder(Color.BLACK, 2));
    	textField1.setMargin(new Insets(0, 0, 0, 0));
    	textField1.setForeground(Color.BLACK);
    	textField1.setBackground(Color.WHITE);
    	textField1.setFont(new Font("Arial", Font.PLAIN, 20));
    	textField1.setEditable(false);
    	textField1.setOpaque(true);
    	textField1.setHorizontalAlignment(JTextField.CENTER);
    	this.add(textField1);
    	this.add(Box.createRigidArea(new Dimension(0,10)));
    	
    	JTextField textField2=new JTextField(winner);
    	textField2.setAlignmentX(Component.CENTER_ALIGNMENT);
    	textField2.setMaximumSize(new Dimension(284, 31));
    	textField2.setBorder(BorderFactory.createLineBorder(Color.BLACK, 2));
    	textField2.setMargin(new Insets(0, 0, 0, 0));
    	textField2.setForeground(Color.BLACK);
    	textField2.setBackground(Color.WHITE);
    	textField2.setFont(new Font("Arial", Font.PLAIN, 20));
    	textField2.setEditable(false);
    	textField2.setOpaque(true);
    	textField2.setHorizontalAlignment(JTextField.CENTER);
    	this.add(textField2);
    	this.add(Box.createRigidArea(new Dimension(0,10)));
	}

	@Override
	protected void paintComponent(Graphics g) {
		super.paintComponent(g);
		g.drawImage(background, 0, 0, this);
	}
}

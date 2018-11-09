package hu.bme.sch.sokoban.game.graphics;

import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.*;

public class Selection extends JPanel {
	private Image background;
	private JButton button;

	private SokobanView parentView;

	JTextField name1 = new JTextField();
	JTextField name2 = new JTextField();

	Selection(SokobanView parentView) {
		this.parentView = parentView;
		this.setLayout(new BoxLayout(this, BoxLayout.PAGE_AXIS));

        try {
            background= ImageIO.read(new File("imageSet" + File.separator + "menu.png"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        
        this.add(Box.createRigidArea(new Dimension(0,50)));

        name1.setAlignmentX(Component.CENTER_ALIGNMENT);
		name1.setMaximumSize(new Dimension(284, 31));
		name1.setBorder(BorderFactory.createLineBorder(Color.BLACK, 2));
		name1.setMargin(new Insets(0, 0, 0, 0));
		name1.setForeground(Color.BLACK);
		name1.setBackground(Color.BLUE);
		name1.setFont(new Font("Arial", Font.PLAIN, 20));
		name1.setEditable(true);
		name1.setOpaque(true);
		this.add(name1);
		this.add(Box.createRigidArea(new Dimension(0,10)));

		name2.setAlignmentX(Component.CENTER_ALIGNMENT);
		name2.setMaximumSize(new Dimension(284, 31));
		name2.setBorder(BorderFactory.createLineBorder(Color.BLACK, 2));
		name2.setMargin(new Insets(0, 0, 0, 0));
		name2.setForeground(Color.BLACK);
		name2.setBackground(Color.RED);
		name2.setFont(new Font("Arial", Font.PLAIN, 20));
		name2.setEditable(true);
		name2.setOpaque(true);
		this.add(name2);

		this.add(Box.createRigidArea(new Dimension(0,10)));
        button = new JButton();
	   	button.setAlignmentX(Component.CENTER_ALIGNMENT);
	   	button.setText("Start");
	   	button.addActionListener(e -> onButtonClick());
	   	this.add(button);
       
	}

	private void onButtonClick() {
		parentView.start(name1.getText(), name2.getText());
	}

	@Override
	protected void paintComponent(Graphics g) {
		super.paintComponent(g);
		g.drawImage(background, 0, 0, this);
	}
}

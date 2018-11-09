package hu.bme.sch.sokoban.game.graphics;

import java.awt.*;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.*;

public class Menu extends JPanel {
    private SokobanView parentView;

	private Image background;
    private JList list;
	private JButton button;

    private MapType mapType = MapType.SMALL;

    Menu(SokobanView parentView) {
	    this.parentView = parentView;

		this.setLayout(new BoxLayout(this, BoxLayout.PAGE_AXIS));
        try {
            background= ImageIO.read(new File("imageSet" + File.separator + "menu.png"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        
        DefaultListModel<String> listModel2 = new DefaultListModel<>();
        listModel2.addElement("Small map");
        listModel2.addElement("Medium map");
        listModel2.addElement("Large map");

        this.add(Box.createRigidArea(new Dimension(0,50)));
        list = new JList<>(listModel2);
        this.add(list);
        list.setAlignmentX(Component.CENTER_ALIGNMENT);
        list.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
        list.setLayoutOrientation(JList.VERTICAL);
        list.setPreferredSize(new Dimension(250, 60));
        list.addListSelectionListener(e -> {
		    if (!e.getValueIsAdjusting()) {
		        if (list.getSelectedIndex() != -1) {
                    final String selectedValue = (String) list.getSelectedValue();

                    switch (selectedValue) {
                        case "Small map":
                            mapType = MapType.SMALL;
                            break;
                        case "Medium map":
                            mapType = MapType.MEDIUM;
                            break;
                        case "Large map":
                            mapType = MapType.LARGE;
                            break;
                    }
		        }
		    }
		});
        
        this.add(Box.createRigidArea(new Dimension(0,50)));
        button = new JButton();
        this.add(button);
        button.setText("Next");
        button.setAlignmentX(Component.CENTER_ALIGNMENT);
        button.addActionListener(e -> onButtonClick());
	}

	private void onButtonClick() {
        String mapName = null;
        switch (mapType) {
            case SMALL:
                mapName = "small_map";
                break;
            case MEDIUM:
                mapName = "medium_map";
                break;
            case LARGE:
                mapName = "large_map";
                break;
        }

        parentView.next(mapName);
    }

    public MapType getMapType() {
        return mapType;
    }

    @Override
	protected void paintComponent(Graphics g) {
		super.paintComponent(g);
		g.drawImage(background, 0, 0, this);
	}
}

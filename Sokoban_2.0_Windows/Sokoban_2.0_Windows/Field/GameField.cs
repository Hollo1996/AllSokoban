using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_2._0_Windows
{
    class GameField
    {
    }
}

/*
 package hu.bme.sch.sokoban.game.map;

import hu.bme.sch.sokoban.Event;
import hu.bme.sch.sokoban.game.commons.Direction;
import hu.bme.sch.sokoban.game.controls.ControlInterface;
import hu.bme.sch.sokoban.game.Game;
import hu.bme.sch.sokoban.game.entities.*;
import hu.bme.sch.sokoban.game.entities.Box;
import hu.bme.sch.sokoban.game.features.*;
import hu.bme.sch.sokoban.game.features.Button;
import hu.bme.sch.sokoban.game.graphics.SokobanView;

import java.awt.*;
import java.io.*;
import java.util.*;

//singleton
public class GameField { //map
    private HashMap<Color, Integer>     scores = new HashMap<>();
    protected ArrayList<ControlInterface> controlInterfaces=new ArrayList<>();
    protected ArrayList<Box>              boxes=new ArrayList<>();
    protected ArrayList<Field>            fields=new ArrayList<>();
    protected ArrayList<double[]>            spawnPlaces=new ArrayList<>();
    protected int spawnedCount=0;

    //singleton stuff
    private static GameField ourInstance = new GameField();
    public static GameField getInstance() {
        return ourInstance;
    }

    protected GameField() {
        // shall create all fields, features and boxes here, also linking them together
    }



    public void loadMap(String filename) throws IOException, WrongCoordException, WrongLineFormat, WrongDirectionFormat {
        BufferedReader br= new BufferedReader(new FileReader(filename+".txt"));

        HashMap<Integer, HashMap<Integer, Field>> fieldsMap = new HashMap<>();
        HashMap<Integer, HashMap<Integer, Box>> boxesMap = new HashMap<>();
        HashMap<Integer, HashMap<Integer, BoxContainer>> boxContainerMap = new HashMap<>();
        HashMap<Integer, HashMap<Integer, FallTrap>> fallTrapMap = new HashMap<>();
        HashMap<Integer, HashMap<Integer, Button>> buttonMap = new HashMap<>();
        HashMap<Integer, HashMap<Integer, Hole>> holeMap = new HashMap<>();
        final short x=0;
        final short y=1;
        int[] coordinates;

        String inputLine;
        String state = "none";
        String[] coordinateParts;
        String[] params;
        while ((inputLine = br.readLine()) != null) {
            if (inputLine.endsWith(":")) { // inputLine is the next state
                state = inputLine;
            }
            else {
                switch (state) {
                    case "fields:":
                        coordinates = coordSplit(inputLine);
                        if (!fieldsMap.containsKey(coordinates[x])) {
                            fieldsMap.put(coordinates[x], new HashMap<>());
                        }

                        Field f = new Field(coordinates[x], coordinates[y]);
                        f.setFriction(1);
                        fieldsMap.get(coordinates[x]).put(coordinates[y], f);
                        break;
                    case "box:":
                        coordinates = coordSplit(inputLine);
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("NOPE");
                        }

                        if (!boxesMap.containsKey(coordinates[x])) {
                            boxesMap.put(coordinates[x], new HashMap<>());
                        }

                        boxesMap.get(coordinates[x]).put(coordinates[y], new Box());
                        fieldsMap.get(coordinates[x]).get(coordinates[y]).addMovable(boxesMap.get(coordinates[x]).get(coordinates[y]));
                        break;
                    case "boxcontainers:":
                        coordinates = coordSplit(inputLine);
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("NOPE");
                        }

                        if (!boxContainerMap.containsKey(coordinates[x])) {
                            boxContainerMap.put(coordinates[x], new HashMap<>());
                        }

                        boxContainerMap.get(coordinates[x]).put(coordinates[y], new BoxContainer());
                        fieldsMap.get(coordinates[x]).get(coordinates[y]).addFeature(boxContainerMap.get(coordinates[x]).get(coordinates[y]));
                        break;
                    case "button:":
                        coordinates = coordSplit(inputLine);
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("NOPE");
                        }

                        if (!buttonMap.containsKey(coordinates[x])) {
                            buttonMap.put(coordinates[x], new HashMap<>());
                        }

                        buttonMap.get(coordinates[x]).put(coordinates[y], new Button());
                        fieldsMap.get(coordinates[x]).get(coordinates[y]).addFeature(buttonMap.get(coordinates[x]).get(coordinates[y]));
                        break;
                    case "falltrap:":
                        coordinates = coordSplit(inputLine);
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine);
                        }

                        if (!fallTrapMap.containsKey(coordinates[x])) {
                            fallTrapMap.put(coordinates[x], new HashMap<>());
                        }

                        fallTrapMap.get(coordinates[x]).put(coordinates[y], new FallTrap(fieldsMap.get(coordinates[x]).get(coordinates[y])));
                        fieldsMap.get(coordinates[x]).get(coordinates[y]).addFeature(fallTrapMap.get(coordinates[x]).get(coordinates[y]));
                        break;
                    case "hole:":
                        coordinates = coordSplit(inputLine);
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine);
                        }

                        if (!holeMap.containsKey(coordinates[x])) {
                            holeMap.put(coordinates[x], new HashMap<>());
                        }

                        holeMap.get(coordinates[x]).put(coordinates[y], new Hole());
                        fieldsMap.get(coordinates[x]).get(coordinates[y]).addFeature(holeMap.get(coordinates[x]).get(coordinates[y]));
                        break;
                    case "neighbourhood:":
                        params = inputLine.split(" ");
                        coordinateParts = params[0].split("-");
                        coordinates = coordSplit(coordinateParts[0]); // parse first part
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine);
                        }

                        Field first = fieldsMap.get(coordinates[x]).get(coordinates[y]);

                        coordinates = coordSplit(coordinateParts[1]); // parse first part


                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine);
                        }

                        Field second = fieldsMap.get(coordinates[x]).get(coordinates[y]);

                        first.addNeighbour(second, stringToDirection(params[2]));
                        second.addNeighbour(first, stringToDirection(params[1]));
                        break;
                    case "wires:":
                        coordinateParts = inputLine.split("-");

                        coordinates = coordSplit(coordinateParts[0]); // parse first part
                        if (!(buttonMap.containsKey(coordinates[x]) && buttonMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine + ". In state:" + state);
                        }

                        Button buttontmp = buttonMap.get(coordinates[x]).get(coordinates[y]);

                        coordinates = coordSplit(coordinateParts[1]); // parse second part
                        if (!(fallTrapMap.containsKey(coordinates[x]) && fallTrapMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine + ". In state:" + state);
                        }

                        FallTrap fallTraptmp = fallTrapMap.get(coordinates[x]).get(coordinates[y]);

                        buttontmp.addSwitchable(fallTraptmp);
                        break;
                    case "compatibility:":
                        coordinateParts = inputLine.split("-");
                        coordinates = coordSplit(coordinateParts[0]); // parse first part
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine);
                        }

                        if (!(boxContainerMap.containsKey(coordinates[x]) && boxContainerMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("NOPE");
                        }

                        BoxContainer boxContainertmp = boxContainerMap.get(coordinates[x]).get(coordinates[y]);

                        coordinates = coordSplit(coordinateParts[1]); // parse second part

                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine);
                        }

                        if (!(boxesMap.containsKey(coordinates[x]) && boxesMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine + ". In state:" + state);
                        }

                        Box boxtmp = boxesMap.get(coordinates[x]).get(coordinates[y]);

                        boxContainertmp.addBox(boxtmp);
                        break;
                    case "spawnplaces:":
                        params = inputLine.split(" ");
                        coordinates = coordSplit(params[0]);
                        if (!(fieldsMap.containsKey(coordinates[x]) && fieldsMap.get(coordinates[x]).containsKey(coordinates[y]))) {
                            throw new WrongCoordException("Wrong Coords added at line: " + inputLine);
                        }

                        double[] dtmp = {(double) coordinates[x], (double) coordinates[y], Double.valueOf(params[1])};
                        spawnPlaces.add(dtmp);
                        break;
                    default:
                        throw new WrongLineFormat("Wrong Status header before line:" + inputLine);
                }
            }
        }

        for (int xc : fieldsMap.keySet()) {
            for (int yc : fieldsMap.get(xc).keySet()) {
                fields.add(fieldsMap.get(xc).get(yc));
            }
        }

        for (int xc : boxesMap.keySet()) {
            for (int yc : boxesMap.get(xc).keySet()) {
                boxes.add(boxesMap.get(xc).get(yc));
            }
        }
    }

    private static int[] coordSplit(String s) throws WrongCoordException {

        if(!s.startsWith("(")||!s.endsWith(")")||s.split(";").length!=2) {
            throw new WrongCoordException("Nope");
        }

        String[] coordinates = s.split("[)(;]");

        int[] coordinatesParsed = new int[2];
        coordinatesParsed[0] = Integer.parseInt(coordinates[1]); // x
        coordinatesParsed[1] = Integer.parseInt(coordinates[2]); // y

        return coordinatesParsed;
    }

    private static Direction stringToDirection(String s) throws WrongDirectionFormat {
        switch (s){
            case "L":return Direction.LEFT;
            case "R":return Direction.RIGHT;
            case "U":return Direction.UP;
            case "D":return Direction.DOWN;
            default: throw new WrongDirectionFormat(s +" stands for no direction");

        }
    }

    // Creates a new control interface
    public ControlInterface createControlInterface(Color c,String up,String right,String down,String left,String putLiquid) {

        Worker w = new Worker(spawnPlaces.get(spawnedCount)[2]);
        for (Field f:fields) {
            if((spawnPlaces.get(spawnedCount)[0] == f.coordX) && (spawnPlaces.get(spawnedCount)[1] == f.coordY)){
                f.addMovable(w);
            }

        }
        // add this worker to a (possibly random) field here -- not sure about this, gotta ask Akos
        ControlInterface ci = new ControlInterface(c, w, up,right,down,left,putLiquid);
        controlInterfaces.add(ci);
        w.setOwner(ci);

        spawnedCount++;

        return ci;
    }

    // Returns the players ranking by iterating through the hash map
    // and incrementing the ranking if someone has a higher score
    public int getPlace(Color c) {
        int place = 1;
        for (Integer other_score: scores.values()) {
            if(scores.get(c) != null) {
                if(other_score > scores.get(c)){
                    place++;
                }
            } else {
                place = 2;
            }
        }
        
        return place;
    }

    // Incrementing the score of the player
    public void score(Color c){
        if(scores.get(c) != null) {
            Integer score = scores.get(c);
            scores.put(c, ++score);
        } else {
            scores.put(c, 1);
        }
    }

    // If any control interface is out of worker, this function gets called
    // If at least one control interface has a worker, returns
    // else, calls the game field's endGame() function
    public void outOfWorkers() {
        for (ControlInterface ci: controlInterfaces) {
            if( ci.hasWorker() )
                return;
        }
        Game.getInstance().endGame();
    }

    // Removes the given box from the boxes list
    public void removeBox(Box box) {
        boxes.remove(box);
    }

    //Called on keyboard input
    public void onEvent(Event event) {
        controlInterfaces.forEach(control -> control.onEvent(event));
    }

    public void setFields(ArrayList<Field> fields) {
        this.fields = fields;
    }

    public void setControlInterfaces(ArrayList<ControlInterface> controlInterfaces) {
        this.controlInterfaces = controlInterfaces;
    }

    public void setBoxes(ArrayList<Box> boxes) {
        this.boxes = boxes;
    }

    public void listWorkers(){
        for ( ControlInterface ci : controlInterfaces ){
            int score = 0;
            if(scores.containsKey(ci.getId())) {
                score = scores.get(ci.getId());
            }
            ci.listWorker(score);
        }
    }

    public void listFeatures(String s){
        for (Field f : fields){
            f.printFeature(s);
        }
    }

    public void listFields(){
        for (Field f : fields){
            f.print();
        }
    }

    public void listBoxes(){
        for (Box b : boxes) {
            b.print();
        }
    }

    public void listFieldFieldConnectivities(){
        int max_row = 20;
        int max_col = 20;

        for(int i = 0; i < max_col; i++){
            for(int k = 0; k < max_row; k++){
                for (Field f: fields) {
                    if(f.coordX == i  &&  f.coordY == k){
                        f.listFieldConnectives();
                    }
                }
            }
        }
    }

    public void listButtonConnectives(){
        for (Field f : fields){
            f.printFeature("buttonConnectives");
        }
    }

    public void drawFields(SokobanView jf) throws IOException {
        for(Field f : fields){
             f.draw(jf);
        }
    }
}
*/

package upperleayer

import console.ConsoleDataRepresentationFactory
import console.charLoader
import controls.Control
import graphics.base.DataRepresentationFactory
import model.ModelContainer
import model.loader.textfile.modelInitializer
import model.loader.textfile.textLoader
import model.moveables.Worker
import basics.Position
import java.io.File

object Game {
    val viewFactory: DataRepresentationFactory = ConsoleDataRepresentationFactory()
    val mapLoader =textLoader
    val graphicLoader= charLoader
    val controlLoader = Control
    var state=GameState.Inicialization

    //Loads Command from a txt
    fun LoadCommand(nameOfTxt: String) = File(nameOfTxt+".txt").useLines { lines -> lines.forEach { CommandHandler(it) } }

    //Reads Commands from the standard input
    fun CommandLoop() {
        var command: String = ""
        while ("Close".compareTo(command) != 0) {
            if ("".compareTo(command) != 0)
                CommandHandler(command)
            command = readLine() ?: ""
        }
    }

    //Handles all command loaded from TXT
    fun CommandHandler(command: String): Boolean {
        val commandAndParams: List<String> = command.split(' ')
        when (commandAndParams[0]) {
            "Start:" -> Start()
            "Stop:" -> Stop()
            "Load:" -> Load()
            "Save:" -> Save()
            else -> return false
        }
        return true
    }

    //Starts a whole new game
    fun Start(): Boolean {
        if (state==GameState.InGame)
            return false

        mapLoader.LoadMap("raw/testmap_moveable03")
        controlLoader.Interface.currentWorker= Worker(modelInitializer.spawnPlaces[0][2].toDouble(), viewFactory.getWorkerdRep())
        ModelContainer.fieldsMap[Position(modelInitializer.spawnPlaces[0][0],modelInitializer.spawnPlaces[0][1])]?.AddMoveable(controlLoader.Interface.currentWorker as Worker)
        graphicLoader.Start()
        controlLoader.readKeyLoop()

        state = GameState.InGame

        return true
    }

    //Stops the current game
    fun Stop(): Boolean {
        return state==GameState.InGame
        //TODO
    }

    //Loads Saved }
    fun Load(): Boolean {
        return state==GameState.Inicialization
        //TODO
    }

    //Saves current Game
    fun Save(): Boolean {
        return state==GameState.InGame
        //TODO
    }


}
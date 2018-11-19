package model

import basics.Position
import model.features.*
import model.map.Field
import model.moveables.Box
import model.moveables.Worker

object ModelContainer{
    val fieldsMap = mutableMapOf<Position, Field>()
    val featuresMap = mutableMapOf<Position, Feature>()
    val boxes = mutableListOf<Box>()
    val workers = mutableListOf<Worker>()
}


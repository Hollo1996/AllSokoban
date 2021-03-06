package model.map

import basics.Direction
import basics.Position
import controls.Liquid
import graphics.base.FieldRepresentation
import model.ModelContainer
import model.features.Feature
import model.moveables.Box
import model.moveables.Moveable
import model.moveables.MoveableVisitor
import model.moveables.Worker
import upperleayer.Game
import upperleayer.GameState

class Field(val position: Position, var representation: FieldRepresentation) : MoveableVisitor {
    data class RepresentableData(
        var Self: Field? = null,
        var onThis: Moveable? = null,
        var functionality: Feature? = null,
        var IsIsolated: Boolean = false,
        var friction: Double = (Liquid.Honey.friction + Liquid.Oil.friction) / 2
    ) {
        private var mod_data = true
        var modified: Boolean
            get() {
                synchronized(mod_data) { return mod_data; }
            }
            set(value) {
                synchronized(mod_data) { mod_data = value; }
            }

    }

    data class TemporaryData(
        var remainedStrength: Double? = null,
        var enter: Direction? = null
    ) {}

    //Most important data
    var rep: RepresentableData = RepresentableData()
    var temp: TemporaryData = TemporaryData()
    var neighbours = mutableMapOf<Direction, Position>()

    init {
        rep.Self = this
        representation.Data = rep

    }

    fun AddNeighbour(position: Position, direction: Direction, forced: Boolean) {
        if (Game.state != GameState.Inicialization || (!forced && neighbours.containsKey(direction)))
            return

        neighbours[direction] = position

        rep.modified = true
    }

    fun AddFeature(_functionality: Feature) {
        if (rep.functionality == null) {
            rep.functionality = _functionality
            rep.modified = true
        }
    }

    // Adds a Moveable object to the field
    fun AddMoveable(moveable: Moveable) {
        if (rep.onThis == null) {
            rep.onThis = moveable
            moveable.SetField(this)
            if (rep.functionality != null)
                rep.functionality?.Interact(moveable)
            rep.modified = true
        }
    }

    operator fun get(d: Direction): Position? = neighbours[d]
    fun IsEmpty(): Boolean = rep.onThis == null
    fun Modified(): Boolean = rep.modified

    override fun Visit(b: Box) {
        var neighbour = ModelContainer.fieldsMap[neighbours[temp.enter] as Position]
        if (neighbours.containsKey(temp.enter) && !(neighbour?.GetIsolated() ?: true)) {
            neighbour?.Push(temp.enter as Direction, b, temp.remainedStrength as Double)
        }
    }

    override fun Visit(w: Worker) {
        var neighbour = ModelContainer.fieldsMap[neighbours[temp.enter] as Position]
        if (neighbours.containsKey(temp.enter) && !(neighbour?.rep?.IsIsolated ?: true) && neighbour?.IsEmpty() ?: false) {
            neighbour?.Push(temp.enter as Direction, w, temp.remainedStrength as Double)
        }
    }


    fun LeaveRequest(direction: Direction, w: Worker) {
        rep.modified = true
        if (!neighbours.containsKey(direction))
            return
        else {
            var theNeighbour = ModelContainer.fieldsMap[neighbours[direction] as Position]
            if (!(theNeighbour?.rep?.IsIsolated?:true)) {
                theNeighbour?.Push(direction, w, w.strength)
                if (rep.onThis == null)
                    rep.modified = true
            }
        }
    }

    fun RemoveMoveable() {
        if (rep.onThis != null) {
            rep.onThis = null
            rep.modified = true
        }
    }

    fun RemoveNeighbour(direction: Direction) {
        if (Game.state!=GameState.Inicialization || !neighbours.containsKey(direction))
            return

        neighbours.remove(direction)
        rep.modified = true
    }

    fun GetIsolated(): Boolean {
        return false
    }

    fun DestroyMoveable() {
        if (rep.onThis != null) {
            rep.onThis?.Destroy()
        }
    }

    // Isolating the field from its neighbours by changing the isIsolated attribute
    fun Isolate() {
        if (rep.IsIsolated != true) {
            rep.IsIsolated = true
            rep.modified = true
        }
    }

    fun Push(direction: Direction, box: Box, remStrength: Double) {
        if (rep.onThis != null) {
            var onThis = rep.onThis
            if (neighbours.containsKey(direction))
                onThis?.Pushed(box, ModelContainer.fieldsMap[neighbours[direction] as Position])
            else
                onThis?.Pushed(box, null)
            if ((remStrength - rep.friction) > 0) {
                temp.enter = direction
                temp.remainedStrength = remStrength - rep.friction
                onThis?.Accept(this)
            }
        }
        if (rep.onThis == null) {
            rep.onThis = box
            box.SetField(this)
            ModelContainer.fieldsMap[neighbours[direction.GetReverse()] as Position]?.RemoveMoveable()
            rep.modified = true

            if (rep.functionality != null) {
                rep.functionality?.Interact(box)
            }
        }
    }

    fun Push(direction: Direction, worker: Worker, remStrength: Double) {
        if (rep.onThis != null) {
            rep.onThis?.Pushed(worker, null)

            if (rep.onThis != null && ((remStrength - rep.friction) > 0)) {
                temp.enter = direction
                temp.remainedStrength = remStrength - rep.friction
                rep.onThis?.Accept(this)
            }
        }

        if (rep.onThis == null) {
            rep.onThis = worker
            worker.SetField(this)
            ModelContainer.fieldsMap[neighbours[direction.GetReverse()] as Position]?.RemoveMoveable()
            rep.modified = true

            if (rep.functionality != null) {
                rep.functionality?.Interact(worker)
            }
        }
    }


}
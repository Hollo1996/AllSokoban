package graphics.base

import basics.Direction
import model.moveables.Worker

interface WorkerRepresentation:MoveableRepresentation {
	var Owner: Worker
	var InDirection: Direction
}
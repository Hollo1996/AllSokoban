package graphics.base

import model.moveables.Box


interface BoxRepresentation:MoveableRepresentation {
	var Owner: Box
	var pushedBy: String?
}
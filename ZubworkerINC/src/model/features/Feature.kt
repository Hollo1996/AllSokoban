package model.features

import graphics.base.FieldRepresentation
import model.moveables.Moveable

interface Feature {
	val representation: FieldRepresentation
	fun Interact(m: Moveable)
}
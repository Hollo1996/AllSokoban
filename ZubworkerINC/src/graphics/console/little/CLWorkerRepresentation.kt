package console.little

import basics.Direction
import graphics.base.WorkerRepresentation
import model.moveables.Worker

class CLWorkerRepresentation : WorkerRepresentation, CLRepresentation {
	override lateinit var Owner: Worker
	override lateinit var InDirection: Direction
	private var _representation =arrayOf<Array<Char>>(arrayOf('W'))
	override var representation = _representation
		get() {
			Owner.LoadRepresentation()
			when (InDirection) {
				Direction.UP -> _representation[0][0] = 'W'
				Direction.DOWN -> _representation[0][0] = 'M'
				Direction.LEFT -> _representation[0][0] = '3'
				Direction.RIGHT -> _representation[0][0] = 'E'
				Direction.NONE -> _representation[0][0] = 'X'
			}
			return _representation

		}
}
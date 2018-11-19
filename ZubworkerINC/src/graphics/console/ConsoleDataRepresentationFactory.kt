package console

import console.little.CLBoxRepresentation
import console.little.CLFieldRepresentation
import console.little.CLWorkerRepresentation
import graphics.base.BoxRepresentation
import graphics.base.DataRepresentationFactory
import graphics.base.WorkerRepresentation

class ConsoleDataRepresentationFactory : DataRepresentationFactory {
	var Big=false
	override fun getWorkerdRep(): WorkerRepresentation {
		return CLWorkerRepresentation()
	}
	override fun getBoxdRep(): BoxRepresentation {
		return CLBoxRepresentation()
	}
	override fun getFieldRep(): CLFieldRepresentation {
		return CLFieldRepresentation()
	}
}
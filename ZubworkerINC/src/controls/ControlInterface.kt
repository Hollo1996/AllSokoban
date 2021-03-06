package controls

import basics.Direction
import model.moveables.Worker

class ControlInterface(val Name:String,var eventKeyMap:Map<ControlKeySettings.ControlEvent, Int>) {
	//The Entity Controled by the player
	var currentWorker: Worker? = null
	fun hasWorker(): Boolean = (currentWorker!=null)
	fun AddWorker(w: Worker) {currentWorker=w}
	fun RemoveWorker() {
		currentWorker=null
		TODO("GameField.OutOfWorkers();")}
	fun listWorker(point:Int)
	{
		if (currentWorker == null)
		{
			return
		}

		//currentWorker?.Print(point)
	}

	//We Handle the key presses
	fun keyHandler(sender:Any,key:Int) :Boolean{
		if(currentWorker==null){return true }
		var triggeredCE:ControlKeySettings.ControlEvent=ControlKeySettings.ControlEvent.None
		for( ce in eventKeyMap.keys) {
			if (eventKeyMap[ce] == key)
				triggeredCE = ce
		}
		when (triggeredCE)
		{
			ControlKeySettings.ControlEvent.None-> return false
			ControlKeySettings.ControlEvent.Up->
				if(currentWorker!=null)
					currentWorker?.Move(Direction.UP)
			ControlKeySettings.ControlEvent.Down->
			if (currentWorker != null)
				currentWorker?.Move(Direction.DOWN)
			ControlKeySettings.ControlEvent.Right->
			if (currentWorker != null)
				currentWorker?.Move(Direction.RIGHT)
			ControlKeySettings.ControlEvent.Left->
			if (currentWorker != null)
				currentWorker?.Move(Direction.LEFT)
			ControlKeySettings.ControlEvent.PutHoney->
			if (currentWorker != null)
				currentWorker?.AddLiquid(Liquid.Honey)
			ControlKeySettings.ControlEvent.PutOil->
			if (currentWorker != null)
				currentWorker?.AddLiquid(Liquid.Oil)
		}
		return true
	}

	fun lost(){}

	fun won(){}
}
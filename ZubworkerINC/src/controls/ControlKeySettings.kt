package controls

import java.awt.event.KeyEvent


class ControlKeySettings() {
    enum class ControlEvent() {
        None(),Up(),Down(),Right(),Left(),PutHoney(),PutOil()

    }
    var  eventKeyMap:MutableMap<ControlEvent, Int> = mutableMapOf<ControlEvent, Int>()
    init
    {
        eventKeyMap[ControlEvent.Up]= KeyEvent.VK_W
        eventKeyMap[ControlEvent.Down]= KeyEvent.VK_S
        eventKeyMap[ControlEvent.Right]= KeyEvent.VK_D
        eventKeyMap[ControlEvent.Left]= KeyEvent.VK_A
        eventKeyMap[ControlEvent.PutHoney]= KeyEvent.VK_H
        eventKeyMap[ControlEvent.PutOil]= KeyEvent.VK_O
    }
    constructor(_keyEventMap:MutableMap<ControlEvent, Int> ):this()
    {
        eventKeyMap[ControlEvent.Up]= KeyEvent.VK_W
        eventKeyMap[ControlEvent.Down]= KeyEvent.VK_S
        eventKeyMap[ControlEvent.Right]= KeyEvent.VK_D
        eventKeyMap[ControlEvent.Left]= KeyEvent.VK_A
        eventKeyMap[ControlEvent.PutHoney]= KeyEvent.VK_H
        eventKeyMap[ControlEvent.PutOil]= KeyEvent.VK_O

        for (ce in _keyEventMap.keys)
            if (_keyEventMap.containsKey(ce))
                eventKeyMap[ce]=_keyEventMap[ce] as Int
    }
}

/*
    public class ControlKeySetting
    {
        public enum ControlEvent : byte
        {
        }


    }
}

*/
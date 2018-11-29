package boss.zubworkersinc.view

import boss.zubworkersinc.basics.Position
import boss.zubworkersinc.graphics.base.FieldRepresentation
import boss.zubworkersinc.graphics.bitmap.tiny.BTFieldRepresentation

object ModelContainer {
    val fieldsMap= mapOf<Position,FieldRepresentation>(
        Pair(Position(0,0),BTFieldRepresentation()),
        Pair(Position(0,1),BTFieldRepresentation()),
        Pair(Position(1,1),BTFieldRepresentation()),
        Pair(Position(1,0),BTFieldRepresentation())
    )

}

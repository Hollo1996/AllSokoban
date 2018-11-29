package boss.zubworkersinc.graphics.bitmap.tiny

import android.graphics.Color
import boss.zubworkersinc.basics.Position
import boss.zubworkersinc.graphics.EasyWeightMap
import boss.zubworkersinc.graphics.base.FieldRepresentation

class BTFieldRepresentation : FieldRepresentation, BTRepresentation {

    private var start = true

    companion object {
        const val empty: Int = Color.BLACK
        private const val syncPortal: Int = Color.BLUE
        private const val asyncPortal: Int = Color.RED
        const val wall: Int = Color.GRAY
        const val corner: Int = Color.GRAY

        private val defRepresentation = EasyWeightMap(
            arrayOf(
                corner, wall, corner,
                wall, empty, wall,
                corner, wall, corner
            ), 3, 0
        )

        private val horizontalWalls = arrayOf(
            EasyWeightMap(arrayOf(empty), 1, wall),
            EasyWeightMap(arrayOf(wall), 1, empty),
            EasyWeightMap(arrayOf(syncPortal), 1, empty),
            EasyWeightMap(arrayOf(asyncPortal), 1, empty)
        )
        private val verticalWalls = arrayOf(
            EasyWeightMap(arrayOf(empty), 1, wall),
            EasyWeightMap(arrayOf(wall), 1, 0),
            EasyWeightMap(arrayOf(syncPortal), 1, 0),
            EasyWeightMap(arrayOf(asyncPortal), 1, 0)
        )

    }

    override val representation = EasyWeightMap(
        arrayOf(
            corner, wall, corner,
            wall, empty, wall,
            corner, wall, corner
        ), 3, 0
    )
        get() {
            Byte.MAX_VALUE
            synchronized(field) {
                field.drawOn(defRepresentation, Position())
                return field
            }
        }
}
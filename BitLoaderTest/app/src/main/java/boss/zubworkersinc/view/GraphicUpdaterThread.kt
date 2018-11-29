package boss.zubworkersinc.view

import android.graphics.*
import boss.zubworkersinc.basics.LifeState
import boss.zubworkersinc.basics.NotifiableThread
import boss.zubworkersinc.basics.Position
import boss.zubworkersinc.graphics.EasyWeightMap
import boss.zubworkersinc.graphics.bitmap.tiny.BTFieldRepresentation

class GraphicUpdaterThread(private val owner: MySurface) : NotifiableThread("Updater") {

    var ourRect = Rect()
    var p = Paint()
    var smaller: Int = 0

    override fun run() {
        super.run()

        var changed = false
        super.run()
        var x = 0
        var canvas: Canvas? = null

        synchronized(owner.Picture) {
            for (col in 0..owner.Picture.columnSize) {
                for (lin in 0..owner.Picture.lineSize) {
                    owner.Picture[col, lin] = 0
                }
            }
        }

        while (owner.cycle.state != LifeState.Created) {
            synchronized(owner.Picture)
            {
                for (mapCord in ModelContainer.fieldsMap.keys) {
                        changed = true
                        owner.Picture.drawOn(
                            (ModelContainer.fieldsMap[mapCord] as BTFieldRepresentation).representation,
                            Position(
                                mapCord.column * owner.tileSizeInPixel.column,
                                mapCord.line * owner.tileSizeInPixel.line
                            )
                        )
                }
            }

            if (changed) {
                synchronized(owner.Picture)
                {
                    if (owner.holder.surface.isValid) {
                        try {
                            synchronized(owner.holder) {
                                canvas = owner.holder.lockCanvas()
                                while (canvas == null) {
                                    System.out.println("canvas is null")
                                    canvas = owner.holder.lockCanvas()
                                }
                            }
                            smaller = if (canvas?.width ?: 0 < canvas?.height ?: 1)
                                canvas?.width ?: 0
                            else
                                canvas?.height ?: 0

                            drawBackGround(canvas)

                            x++
                            PaintIt(canvas)
                            println(x.toString())

                        } finally {
                            if (canvas != null) {
                                synchronized(owner.holder) {
                                    owner.holder.unlockCanvasAndPost(canvas)
                                }
                            }
                        }
                    }
                }
                changed = false
            }
        }
    }

    private fun drawBackGround(canvas: Canvas?) {
        canvas?.drawARGB(255, 0, 0, 0)

        ourRect = Rect(0, 0, smaller, smaller)
        p = Paint()
        p.color = Color.BLUE
        p.style = Paint.Style.FILL
        canvas?.drawRect(ourRect, p)
    }

    private fun PaintIt(canvas: Canvas?) {
        val bmp = Bitmap.createBitmap(owner.Picture.columnSize, owner.Picture.lineSize, Bitmap.Config.ARGB_8888)
        owner.Picture.copyToBitmap(bmp)
        val canRect = Rect(0, 0, smaller, smaller)
        canvas?.drawBitmap(bmp, null, canRect, null)

    }


    private fun EasyWeightMap<Int>.copyToBitmap(bmp: Bitmap) {
        for (col in 0..(columnSize - 1))
            for (lin in 0..(lineSize - 1)) {
                bmp.setPixel(col, lin, this[col, lin])
            }
    }
}
package boss.zubworkers.graphics.bitmap

import android.graphics.*
import boss.zubworkers.basics.LoaderState
import boss.zubworkersinc.basics.NotifiableThread
import boss.zubworkersinc.basics.Position
import boss.zubworkersinc.graphics.bitmap.BitLoader
import boss.zubworkersinc.graphics.bitmap.EasyWeightBitMap
import java.lang.Exception
import kotlin.concurrent.thread

class PainterThread(private val owner: BitLoader) : NotifiableThread("Painter") {


    var ourRect = Rect()
    var p = Paint()
    var smaller: Int = 0


    override fun run() {
        super.run()
        var x = 0
        var canvas: Canvas? = null

        owner.updater?.Notify("Paint")
        while (owner.running != LoaderState.Created) {
            bufferLock.Wait()
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
                        if (canvas?.width ?: 0 < canvas?.height ?: 1)
                            smaller = canvas?.width ?: 0
                        else
                            smaller = canvas?.height ?: 0

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
        }
    }

    fun drawBackGround(canvas: Canvas?) {
        canvas?.drawARGB(255, 0, 0, 0)

        ourRect = Rect(0, 0, smaller, smaller)
        p = Paint()
        p.color = Color.BLUE
        p.style = Paint.Style.FILL
        canvas?.drawRect(ourRect, p)
    }

    private fun PaintIt(canvas: Canvas?) {
        val bmp=Bitmap.createBitmap(owner.Picture.columnSize,owner.Picture.lineSize,Bitmap.Config.ARGB_8888)
        owner.Picture.copyToBitmap(bmp)
        val canRect = Rect(0, 0, smaller , smaller )
        canvas?.drawBitmap(bmp, null, canRect, null)

    }
}
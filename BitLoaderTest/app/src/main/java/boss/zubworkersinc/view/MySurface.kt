package boss.zubworkersinc.view

import android.content.Context
import android.util.AttributeSet
import android.view.SurfaceHolder
import android.view.SurfaceView
import boss.zubworkersinc.basics.LifeCicle
import boss.zubworkersinc.basics.Position
import boss.zubworkersinc.graphics.EasyWeightMap

class MySurface : SurfaceView {


    constructor(context: Context) : super(context)
    constructor(context: Context, attrs: AttributeSet) : super(context, attrs)
    constructor(context: Context, attrs: AttributeSet, defStyleAttr: Int) : super(context, attrs, defStyleAttr)

    val cycle: LifeCicle = object : LifeCicle() {
        val ms: MySurface = this@MySurface
        override fun createInner() {
            justStart = true
            graphicUpdater = GraphicUpdaterThread(ms)
        }

        override fun destroyInner() {
            graphicUpdater = null
        }

        override fun startInner() {
            if (graphicUpdater?.isAlive == false)
                graphicUpdater?.start()
        }

        override fun stopInner() {
            var retryp = true
            while (retryp) {
                try {
                    graphicUpdater?.join()
                    retryp = false
                } catch (e: InterruptedException) {
                    e.printStackTrace()
                }
            }
            System.out.println("Stopped!!")
        }

        override fun pauseInner() {
        }

        override fun resumeInner() {
            graphicUpdater?.Notify("Resume")
        }
    }

    init {
        holder.addCallback(object : SurfaceHolder.Callback {
            override fun surfaceCreated(holder: SurfaceHolder) {
                // empty
            }

            override fun surfaceDestroyed(holder: SurfaceHolder) {
                //empty
            }

            override fun surfaceChanged(holder: SurfaceHolder, format: Int, width: Int, height: Int) {
                //empty
            }
        })
    }

    var graphicUpdater: GraphicUpdaterThread? = null
    var justStart = true


    val tileSizeInPixel = Position(3, 3)
    val pictureSizeInTile: Position = Position(2,2)
    val Picture: EasyWeightMap<Int> = EasyWeightMap(
        pictureSizeInTile.column * tileSizeInPixel.column,
        pictureSizeInTile.line * tileSizeInPixel.line,
        0
    )


    @Synchronized
    override fun invalidate() {
        graphicUpdater?.Notify("BitLoader:Invalidate")
    }

}
package boss.zubworkersinc.graphics.bitmap

import android.content.Context
import android.graphics.*
import android.util.AttributeSet
import android.view.SurfaceHolder
import android.view.SurfaceView
import boss.zubworkers.basics.LoaderState
import boss.zubworkers.graphics.base.GraphicLoader
import boss.zubworkers.graphics.bitmap.PainterThread
import boss.zubworkers.graphics.bitmap.UpdaterThread
import boss.zubworkersinc.basics.BuffererLock
import boss.zubworkersinc.basics.Position
import java.lang.Exception
import java.util.*
import kotlin.concurrent.thread

class BitLoader : SurfaceView, GraphicLoader {


    constructor(context: Context) : super(context)
    constructor(context: Context, attrs: AttributeSet) : super(context, attrs)
    constructor(context: Context, attrs: AttributeSet, defStyleAttr: Int) : super(context, attrs, defStyleAttr)

    val upModifierLock: Int = 0
    var justStart = true

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

    override fun Invalidate() {
        TODO("not implemented") //To change body of created functions use File | Settings | File Templates.
    }

    var running = LoaderState.Destroyed

    var updater: UpdaterThread? = null
    var painter: PainterThread? = null


    var tileSizeInPixel = Position(5, 5)
    override var pictureSizeInTile: Position = Position(4,4)
    var Picture: EasyWeightBitMap = EasyWeightBitMap(pictureSizeInTile.column*tileSizeInPixel.column,pictureSizeInTile.line*tileSizeInPixel.line)

    var size: Int = -1

    override fun create() {
        System.out.println("Create!!")
        synchronized(running) {
            if (running != LoaderState.Destroyed)
                return
            else
                running = LoaderState.Created
        }
        justStart = true
        updater = UpdaterThread(this)
        painter = PainterThread(this)
    }

    override fun destroy() {
        System.out.println("Destroy!!")
        synchronized(running) {
            if (running != LoaderState.Created)
                return
            else
                running = LoaderState.Destroyed
        }
        painter = null
        updater = null
    }

    override fun start() {
        System.out.println("Start!!")

        create()

        synchronized(running) {
            create()
            if (running != LoaderState.Created)
                return
            else
                running = LoaderState.Started
        }

        if (updater?.isAlive == false)
            updater?.start()
        if (painter?.isAlive == false)
            painter?.start()
    }

    override fun stop() {
        System.out.println("Stop!!")
        synchronized(running) {
            if (running != LoaderState.Started)
                return
            else
                running = LoaderState.Created
        }
        var retryu = true
        var retryp = true
        while (retryu || retryp) {
            try {
                updater?.join()
                retryu = false
            } catch (e: InterruptedException) {
                e.printStackTrace()
            }
            try {
                painter?.join()
                retryp = false
            } catch (e: InterruptedException) {
                e.printStackTrace()
            }
        }
        destroy()
        System.out.println("Stopped!!")
    }

    override fun pause() {
        System.out.println("Pause!!")
        synchronized(running) {
            if (running != LoaderState.Resumed)
                return
            else
                running = LoaderState.Started
        }
    }

    override fun resume() {
        System.out.println("Resume!!")
        synchronized(running) {
            if (running != LoaderState.Started)
                return
            else
                running = LoaderState.Resumed
        }
        updater?.Notify("Resume")
        painter?.Notify("Resume")
    }

}
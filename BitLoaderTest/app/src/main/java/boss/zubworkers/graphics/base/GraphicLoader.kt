package boss.zubworkers.graphics.base
import boss.zubworkersinc.basics.Position

interface GraphicLoader {
    var pictureSizeInTile: Position

    fun create()
    fun destroy()
    fun start()
    fun stop()
    fun pause()
    fun resume()

    fun Invalidate()
}
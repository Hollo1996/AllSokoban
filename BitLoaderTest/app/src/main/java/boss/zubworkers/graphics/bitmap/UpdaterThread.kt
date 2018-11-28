package boss.zubworkers.graphics.bitmap

import boss.zubworkers.basics.LoaderState
import boss.zubworkersinc.basics.NotifiableThread
import boss.zubworkersinc.basics.Position
import boss.zubworkersinc.graphics.bitmap.BitLoader
import boss.zubworkersinc.graphics.bitmap.EasyWeightBitMap
import java.util.*

class UpdaterThread(private val owner: BitLoader) : NotifiableThread("Updater") {

    private val colors= intArrayOf(0xFFFF0000.toInt(),0xFF00FF00.toInt(),0xFF0000FF.toInt())

    override fun run() {
        super.run()


        //println("Running is Checked")

        var changed = false
        val r = Random()
        synchronized(owner.Picture) {
            for (col in 0..owner.Picture.columnSize) {
                for (lin in 0..owner.Picture.lineSize) {
                    owner.Picture[col, lin] = 0
                }
            }
        }
        //println("owner.Picture is reseted by Update")
        while (owner.running != LoaderState.Created) {
            synchronized(owner.Picture)
            {
                ////println("\n I jast started Updating!")
                for (col in 0..3) {
                    for (lin in 0..3) {
                        //if (r.nextInt() % 4 != 0)
                        //    break
                        changed = true
                        owner.Picture.DrawOn(
                            EasyWeightBitMap(
                                intArrayOf(
                                    r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(),
                                    r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(),
                                    r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(),
                                    r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(),
                                    r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt(), r.nextInt()
                                ), 5
                            ),
                            Position(col*owner.tileSizeInPixel.column, lin*owner.tileSizeInPixel.line)
                        )
                    }
                }
            }
            if (changed) {
                owner.painter?.Notify("Update")
                changed = false
            }
        }
//println("Update Stopped")
    }
}
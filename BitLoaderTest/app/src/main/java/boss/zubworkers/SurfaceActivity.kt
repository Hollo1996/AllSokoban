package boss.zubworkers

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import boss.zubworkersinc.graphics.bitmap.BitLoader
import kotlinx.android.synthetic.main.activity_surface.*

class SurfaceActivity : AppCompatActivity() {
    
    var v:BitLoader?=null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_surface)
        v=surfaceBL
    }

    override fun onDestroy() {
        super.onDestroy()
    }

    override fun onStart() {
        super.onStart()
        v?.start()
    }

    override fun onStop() {
        v?.stop()
        super.onStop()
    }

    override fun onResume() {
        super.onResume()
        v?.resume()
    }

    override fun onPause() {
        v?.pause()
        super.onPause()
    }
}

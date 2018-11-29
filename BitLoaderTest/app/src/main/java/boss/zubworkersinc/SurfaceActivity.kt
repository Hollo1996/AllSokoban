package boss.zubworkersinc

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import boss.zubworkersinc.view.MySurface
import kotlinx.android.synthetic.main.activity_surface.*

class SurfaceActivity : AppCompatActivity() {
    
    var v:MySurface?=null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_surface)
        v=surfaceMS
    }

    override fun onStart() {
        super.onStart()
        v?.cycle?.start()
    }

    override fun onStop() {
        v?.cycle?.stop()
        super.onStop()
    }

    override fun onResume() {
        super.onResume()
        v?.cycle?.resume()
    }

    override fun onPause() {
        v?.cycle?.pause()
        super.onPause()
    }
}

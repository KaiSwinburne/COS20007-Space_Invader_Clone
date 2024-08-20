using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_project
{
    public class CountdownScene : Level
    {
        private int _seconds, _coundowntime;
        private SplashKitSDK.Timer _countdown;
        private uint _checkpoint;
        public CountdownScene(int time) : base(new string[] { "countdown", "unplayable" })
        {
            Selected = false;
            IsPaused = false;
            
            _seconds = time;
            _coundowntime = time;
            _countdown = SplashKit.CreateTimer("countdown");
            _checkpoint = _countdown.Ticks;
        }
        public override void Draw()
        {           
            if (_seconds < 1)
            {
                SplashKit.DrawTextOnWindow(Demo.window,"START", Color.White, Demo.font, 60, Demo.window.Width/ 2 - 80, 400);
            }
            else
            {
                SplashKit.DrawTextOnWindow(Demo.window, _seconds.ToString(), Color.White, Demo.font, 60, Demo.window.Width / 2, 400);
            }
        }

        public override void Reset()
        {
            _countdown.Stop();
            _countdown.Reset();
            _seconds = _coundowntime;
            _checkpoint = _countdown.Ticks;
        }

        public override void Update()
        {
            uint now = _countdown.Ticks;

            //every 1 second passes
            if ((now - _checkpoint) > 1000)
            {
                _seconds -= 1;
                _checkpoint = now;
            }

            if (_seconds < 0)
            {
                Reset();
                Selected = false;
            }
        }


        public SplashKitSDK.Timer Timer
        {
            get { return _countdown; }
        }
    }
}

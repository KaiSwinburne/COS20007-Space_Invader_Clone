using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class StartMenu : Level
    {
        private Button _start, _quit;
        public StartMenu():base(new string[] { "startmenu", "unplayable" })
        {
            Selected = true;
            _start = new Button("Start",45, 150, 60, 300, 300);
            _quit = new Button("Quit", 45, 130, 60, 300, 360);
        }

        public override void Draw()
        {
            int widthmid = Demo.window.Width / 2 - 90 / 2 - 100;
            SplashKit.DrawTextOnWindow(Demo.window, "SPACE", Color.White, Demo.font, 90, widthmid, Demo.window.Height-550);
            SplashKit.DrawTextOnWindow(Demo.window, "CONQUERORS +", Color.White, Demo.font, 90, widthmid-120, Demo.window.Height - 460);
            SplashKit.DrawTextOnWindow(Demo.window, "by Thanh Tai Tran", Color.White, Demo.font, 24, Demo.window.Width - 250, Demo.window.Height - 24);
            _start.Draw();
            _quit.Draw();
        }

        public override void Reset()
        {
            return;
        }

        public override void Update()
        {
            if (_start.IsClicked)
            {               
                Demo.levels.UnloadLevel("startmenu");
                Demo.levels.ResetLevel("level1");
                Demo.levels.LoadLevel("countdown");
                Demo.levels.UnpauseLevel("level1");
                Demo.levels.BeginCountdownTimer(Demo.levels.ReturnLevel("level1") as IHaveCountdownBeforeStart);
                Demo.levels.LoadLevel("level1");
            }
            _start.Update(SplashKit.MousePosition());            
            _quit.Update(SplashKit.MousePosition());
        }
    }
}

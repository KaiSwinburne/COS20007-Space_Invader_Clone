using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class PauseMenu : Level
    {
        private Button _resume, _desktop, _backtomainmenu, _reset;
        private Rectangle _overlay;
        public PauseMenu():base(new string[] {"pausemenu","unplayable"})
        {
            Selected = false;
            IsPaused = false;
            _overlay = new Rectangle();
            _overlay.X = _overlay.Y = 0;
            _overlay.Width = Demo.window.Width;
            _overlay.Height = Demo.window.Height;

            _resume = new Button("RESUME", 45, 180, 60, 300, 210);
            _reset = new Button("RESET", 45, 180, 60, 300, 280);
            _backtomainmenu = new Button("BACK TO MENU", 45, 340, 60, 240, 350);
            _desktop = new Button("QUIT TO DESKTOP", 45, 400, 60, 220, 420);
        }

        public override void Draw()
        {
            SplashKit.FillRectangleOnWindow(Demo.window, SplashKit.RGBAColor(0, 0, 0, 0.6),_overlay);
            SplashKit.DrawTextOnWindow(Demo.window, "PAUSED", Color.White, Demo.font, 60, 300, 100);           
            _resume.Draw();
            _reset.Draw();
            _backtomainmenu.Draw();
            _desktop.Draw();
        }

        public override void Reset()
        {
            return;
        }

        public override void Update()
        {
            _resume.Update(SplashKit.MousePosition());
            if (_resume.IsClicked)
            {
                Demo.levels.UnloadLevel("pausemenu");
                Demo.levels.UnpauseCurrentLevel();
            }
            if (_backtomainmenu.IsClicked)
            {                
                Demo.levels.UnloadCurrentLevel();
                Demo.levels.LoadLevel("startmenu");
                Demo.levels.UnloadLevel("pausemenu");
            }
            if (_reset.IsClicked)
            {
                Demo.levels.UnpauseCurrentLevel();
                Demo.levels.UnloadLevel("pausemenu");
                Demo.levels.ResetCurrentLevel();
                Demo.levels.LoadLevel("countdown");
                Demo.levels.BeginCountdownTimer(Demo.levels.ReturnCurrentLevel() as IHaveCountdownBeforeStart);
            }

            _backtomainmenu.Update(SplashKit.MousePosition());
            _desktop.Update(SplashKit.MousePosition());
            _reset.Update(SplashKit.MousePosition());
        }
    }
}

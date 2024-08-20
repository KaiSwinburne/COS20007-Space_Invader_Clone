using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_project
{
    public class GameOverScene : Level
    {
        private Button _restart, _nextlevel, _mainmenu;
        private Rectangle _overlay;
        private Player _player;
        private bool _youwin;
        public GameOverScene(Player player):base(new string[] { "gameover", "unplayable" })
        {
            Selected = false;
            IsPaused = false;
            _youwin = false;
            _overlay = new Rectangle();
            _overlay.X = _overlay.Y = 0;
            _overlay.Width = Demo.window.Width;
            _overlay.Height = Demo.window.Height;
            _player = player;

            _nextlevel = new Button("NEXT LEVEL", 45, 280, 60, 270, 300);
            _restart = new Button("RESTART", 45, 220, 60, 300, 370);
            _mainmenu = new Button("MAIN MENU", 45, 250, 60, 280, 440);
            
        } 
        public override void Draw()
        {
            SplashKit.FillRectangleOnWindow(Demo.window, SplashKit.RGBColor(0, 0, 0), _overlay);
            switch (_youwin)
            {
                default:                  
                    SplashKit.DrawTextOnWindow(Demo.window, "GAME OVER", Color.White, Demo.font, 70, 230, 100);
                    SplashKit.DrawTextOnWindow(Demo.window, "POINTS: " + _player.Points, Color.White, Demo.font, 60, 230, 200);
                    _restart.Draw();
                    _mainmenu.Draw();
                    break;
                case true:
                    SplashKit.DrawTextOnWindow(Demo.window, "YOU SURVIVED", Color.White, Demo.font, 70, 230, 100);
                    SplashKit.DrawTextOnWindow(Demo.window, "POINTS: " + _player.Points, Color.White, Demo.font, 60, 230, 200);
                    _nextlevel.Draw();
                    _restart.Draw();
                    _mainmenu.Draw();
                    break;
            }
            
        }

        public override void Reset()
        {

        }

        public override void Update()
        {
            _restart.Update(SplashKit.MousePosition());
            _mainmenu.Update(SplashKit.MousePosition());
            switch (_youwin)
            {
                default:
                    break;
                case true:
                    _nextlevel.Update(SplashKit.MousePosition());
                    break;
            }
            if (_restart.IsClicked)
            {
                Demo.levels.UnpauseCurrentLevel();
                Demo.levels.UnloadLevel("gameover");
                Demo.levels.ResetCurrentLevel();
                Demo.levels.LoadLevel("countdown");
                Demo.levels.BeginCountdownTimer(Demo.levels.ReturnCurrentLevel() as IHaveCountdownBeforeStart);
            }
            if (_mainmenu.IsClicked)
            {
                Demo.levels.UnloadCurrentLevel();
                Demo.levels.LoadLevel("startmenu");
                Demo.levels.UnloadLevel("gameover");
            }
            
        }

        public bool PlayerWon
        {
            set { _youwin = value; }
            get { return _youwin; }
        }
    }
}

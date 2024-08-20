using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_project
{
    public class HUD
    {
        private int _fontsize;
        private Rectangle _hudbackground;
        private Bitmap _playericon;
        public HUD()
        {
            _playericon = SplashKit.LoadBitmap("icon", "HUDicon.png");
            _fontsize = 25;

            _hudbackground = new Rectangle();
            _hudbackground.X = 0;
            _hudbackground.Y = 0;
            _hudbackground.Width = Demo.window.Width;
            _hudbackground.Height = 40;
        }
        public void Draw(Player player)
        {
            //mid y coordinate in relation to the hud height
            double y_midpoint = (_hudbackground.Height - _fontsize) / 2;

            double y_bitmapmidpoint = (_hudbackground.Height - _playericon.Height) / 2;
            SplashKit.FillRectangle(Color.Black, _hudbackground);
            SplashKit.DrawBitmapOnWindow(Demo.window,_playericon, _hudbackground.Width - _playericon.Width - _fontsize*3, y_bitmapmidpoint);
            foreach (Level level in Demo.levels.Listing)
            {
                if (level.Selected && level.AreYou("playable"))
                {
                    SplashKit.DrawTextOnWindow(Demo.window, level.LevelName, Color.White, Demo.font, _fontsize, _hudbackground.Width/2 - _fontsize, y_midpoint);
                }
            }
            SplashKit.DrawTextOnWindow(Demo.window, "X " + player.HealthBar.Health, Color.White, Demo.font, _fontsize, _hudbackground.Width - _playericon.Width-_fontsize, y_midpoint);
            SplashKit.DrawTextOnWindow(Demo.window, "Points: " + player.Points, Color.White, Demo.font, _fontsize, _hudbackground.X, y_midpoint);
        }
    }
}

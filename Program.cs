using System;
using SplashKitSDK;

namespace Custom_project
{
    public class Program
    {
        public static float deltatime;
        public static uint targetFPS;
        public static void Main()
        {
            Window window = new Window("Space Conquerors plus", 800, 600);
            Demo demo = new Demo(window);
            SplashKitSDK.Timer timer = SplashKit.CreateTimer("deltatime");
            timer.Start();
            uint prev_time = timer.Ticks;
            targetFPS = 60;
            do
            {
                uint now = timer.Ticks;
                deltatime = Convert.ToSingle((now - prev_time)/1000.0);
                SplashKit.RefreshScreen(60);
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.Black);
                //update the game
                demo.Update();
                //draw the game
                demo.Draw();                
                
                prev_time = now;
            }
            while (!window.CloseRequested);
        }
    }
}

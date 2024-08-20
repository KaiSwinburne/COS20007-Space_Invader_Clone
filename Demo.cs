using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Custom_project
{
    public class Demo
    {
        private Player _player;
        public static Window window;

        public static Font font;       
        public static LevelList levels;
        public Demo(Window win)
        {           
            window = win;
            font = SplashKit.LoadFont("Points font", "gameovercre1.ttf");
            levels = new LevelList();
            _player = new Player("Player","test player");

            Level countdown = new CountdownScene(3);
            levels.AddLevel(countdown);
            Level gameover = new GameOverScene(_player);
            levels.AddLevel(gameover);

            Level level1 = new Level1(_player);
            Level StartMenu = new StartMenu();
            Level PauseMenu = new PauseMenu();

            levels.AddLevel(level1);
                    
            levels.AddLevel(StartMenu);
            levels.AddLevel(PauseMenu);
        }

        // Update the game in real time
        public void Update()
        {
            levels.Update();     
        }

        //Draw the objects onto the screen
        public void Draw()
        {
            levels.Draw();           
        }

    }

}

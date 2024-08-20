using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class Level1 : Level, IHaveCountdownBeforeStart
    {
        private EnemiesTable _table;
        private HUD _playerHUD;
        private Player _player;
        private CountdownScene _countdown;
        private GameOverScene _overscreen;
        private bool _playerlose;

        public Level1(Player player):base(new string[] {"level1","playable"})
        {
            _playerlose = false;
            Selected = false;
            IsPaused = false;

            _countdown = Demo.levels.ReturnLevel("countdown") as CountdownScene;
            _overscreen = Demo.levels.ReturnLevel("gameover") as GameOverScene;

            _player = player;
            
            _playerHUD = new HUD();

            _player.Sprite.X = Demo.window.Width/2;
            _player.Sprite.Y = Demo.window.Width - _player.Sprite.Height;

            _table = new EnemiesTable();
                      
            _table.SpawnEnemiesTable();           
        }

        public override void Draw()
        {
            //draw all the elements first before drawing the countdown
            if (!_playerlose)
            {
                if (_table.Enemies.Count > 0)
                {
                    _table.Draw();
                } 
                _player.Draw();
                _playerHUD.Draw(_player);
            }         

            if (_countdown.Selected)
            {
                _countdown.Draw();
                return;
            }
        }

        public override void Update()
        {
            //at the beggining of the level, countdown scene is selected
            if (_countdown.Selected)
            {
                _countdown.Update();
                return;
            }

            if (SplashKit.KeyTyped(KeyCode.EscapeKey))
            {
                IsPaused = true;                
            }

            //player lose
            if (_player.HealthBar.Health == 0)
            {
                _overscreen.Selected = true;
                _playerlose = true;
            }

            //player win
            if (_table.Enemies.Count == 0)
            {
                _overscreen.PlayerWon = true;
                _overscreen.Selected = true;
                _playerlose = false;
            }

            switch (IsPaused)
            {
                default:
                    ResumeAllTimer();
                    if (_table.Enemies.Count > 0 && _player.HealthBar.Health>0)
                    {
                        _table.Update(_player);
                    }                    
                    _player.Update(_table.Enemies);
                    break;
                case true:
                    //when paused, load pause menu
                    PauseAllTimer();
                    Demo.levels.LoadLevel("pausemenu");                    
                    break;
            }         
        }

        public void PauseAllTimer()
        {
            _player.AttackTimer.Pause();
            _player.InvincibilityTimer.Pause();
            _player.BlinkTimer.Pause();
            _table.AttackTimer.Pause();
        }

        public void ResumeAllTimer()
        {
            _player.AttackTimer.Resume();
            _player.InvincibilityTimer.Resume();
            _player.BlinkTimer.Resume();
            _table.AttackTimer.Resume();
        }

        public override void Reset()
        {
            _playerlose = false;
            _table.Reset();
            _player.Reset();
            _player.Sprite.X = Demo.window.Width / 2;
            _player.Sprite.Y = Demo.window.Width - _player.Sprite.Height;
        }


        public SplashKitSDK.Timer CountdownTimer
        {
            get { return _countdown.Timer; }
        }

    }
}

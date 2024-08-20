using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class Player : Actor, IHavePoints, ICanBeReset
    {
        private BulletPool _bulletpool;
        private int _playerpoints, _damage;
        private bool _isivincible;
        private List<PlayerBullets> _bullets = new List<PlayerBullets>();
        private static SplashKitSDK.Timer _attacktimer, _invicibilitytimer, _blinktimer;

        public Player(string name, string description):base(name,description,5)
        {
            Bitmap = SplashKit.LoadBitmap("player", "player.png");
            Sprite = SplashKit.CreateSprite("Player", Bitmap);

            _isivincible = false;
            _damage = 4;

            _attacktimer = SplashKit.CreateTimer("Player's attack timer");
            _attacktimer.Start();

            _invicibilitytimer = SplashKit.CreateTimer("Player's invicibility");
            _blinktimer = SplashKit.CreateTimer("blink time");

            _playerpoints = 0;

            _bulletpool = new BulletPool("player",20);
            FireRate = 50;
            
            DamageCollisionWidth = 10;
            DamageCollisionHeight = 10;

            Speed = 5;
        }

        public override void Draw()
        {
            switch (_isivincible)
            {
                default:
                    SplashKit.DrawSprite(Sprite);
                    break;
                case true:
                    DrawPlayerFlashes();
                    break;
            }
            
            //SplashKit.DrawRectangle(Color.Red, DamageCollision);
            //SplashKit.DrawRectangle(Color.White, Sprite.CollisionRectangle);
            
            foreach (Bullets bullet in _bullets)
            {
                bullet.Draw();
            }
        }

        public void Update(List<Enemies> enemies)
        {
            //Pause the timer when it's over the firerate, so when the player first press attack, it fires immediately
            if (_attacktimer.Ticks > FireRate)
            {
                _attacktimer.Pause();     
            }

            Movement();       
                      
            //everytime we press space, fire
            if (SplashKit.KeyTyped(KeyCode.SpaceKey) && _isivincible==false)
            {
                _attacktimer.Resume();
                if (_attacktimer.Ticks > FireRate)
                {
                    ShootingBullets();
                    _attacktimer.Reset();
                }             
            }

            //Make sure the timer is bigger than the firerate when firebutton is realeased
            if (SplashKit.KeyReleased(KeyCode.SpaceKey))
            {
                if (_attacktimer.Ticks > FireRate)
                {
                    _attacktimer.Pause();
                }
            }

            if (_isivincible == false)
            {
                GetDamaged(enemies);
            }

            if (_invicibilitytimer.Ticks > 5000)
            {
                _isivincible = false;
                _invicibilitytimer.Stop();
                _invicibilitytimer.Reset();
                _blinktimer.Stop();
                _blinktimer.Reset();
            }

            BulletsUpdate();
        }

        public override void ShootingBullets()
        {
            //get the bullet from the bullet pool
            PlayerBullets bullet = _bulletpool.GetBullets() as PlayerBullets;

            //initialize the bullet position with the player's position
            bullet.Sprite.X = this.Sprite.X+Sprite.Width/2;
            bullet.Sprite.Y = this.Sprite.Y-Sprite.Height/2;

            _bullets.Add(bullet);
        }
        
        public void BulletsUpdate()
        {         
            foreach (PlayerBullets bullet in _bullets)
            {
                bullet.Update();
            }

            //if bullet is out of screen, return it to the bullet pool
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].BulletOutOfScreen())
                {
                    PlayerBullets recyclebullet = _bullets[i]; 
                    _bulletpool.ReturnBullets(recyclebullet);
                    _bullets.Remove(_bullets[i]);
                    break;
                }
            }
        }

        public void Movement()
        {
            if (SplashKit.KeyDown(KeyCode.AKey))
            {
                Sprite.X += -Speed* Program.deltatime * Program.targetFPS;                               
            }
            
            if (SplashKit.KeyDown(KeyCode.DKey))
            {
                Sprite.X += Speed * Program.deltatime * Program.targetFPS;
            }
            StayOnWindow();
            DamageCollisionX = Sprite.X + (Sprite.Width / 2 - DamageCollisionWidth / 2);
            DamageCollisionY = Sprite.Y + (Sprite.Height / 2 - DamageCollisionHeight / 2);
        }

        //make sure the player does not leave the screeen
        public void StayOnWindow()
        {
            if (Sprite.X < 0)
            {
                Sprite.X = 0;
            }
            if (Sprite.Y < 0)
            {
                Sprite.Y = 0;
            }
            if (Sprite.X > Demo.window.Width - Sprite.Width)
            {
                Sprite.X = Demo.window.Width - Sprite.Width;
            }
            if (Sprite.Y > Demo.window.Height - Sprite.Height)
            {
                Sprite.Y = Demo.window.Height - Sprite.Height;
            }
        }

        
        //if the player receives damage from the enemy
        public void GetDamaged(List<Enemies> enemies)
        {    
            for (int i=0; i<enemies.Count; i++)
            {
                foreach (EnemyBullets enemybullet in enemies[i].Bullets)
                {
                    if (SplashKit.RectanglesIntersect(enemybullet.Sprite.CollisionRectangle, this.DamageCollision))    //this will decrease the health everytime the bullet hit it hitbox
                    {
                        enemies[i].BulletPool.ReturnBullets(enemybullet);
                        enemies[i].Bullets.Remove(enemybullet);
                        HealthBar.Health -= 1;
                        Points -= 10;
                        _isivincible = true;
                        _invicibilitytimer.Start();
                        _blinktimer.Start();
                        break;
                    }
                }
            }         
        }

        //this occurs when the player is invincible
        public void DrawPlayerFlashes()
        {
            if (_blinktimer.Ticks > 50)
            {
                SplashKit.DrawSprite(Sprite);
                _blinktimer.Reset();
            }
        }

        //reset the player
        public void Reset()
        {
            _attacktimer.Reset();

            _invicibilitytimer.Stop();
            _invicibilitytimer.Reset();

            _blinktimer.Stop();
            _blinktimer.Reset();

            _isivincible = false;

            _playerpoints = 0;
            HealthBar.Health = HealthBar.MaxHealth;
        }

        public List<PlayerBullets> Bullets
        {
            get { return _bullets; }
        }

        public BulletPool BulletPool
        {
            get { return _bulletpool; }
        }

        public int Points
        {
            get { return _playerpoints; }
            set { _playerpoints = value; }
        }

        public SplashKitSDK.Timer AttackTimer
        {
            get { return _attacktimer; }
        }

        public SplashKitSDK.Timer InvincibilityTimer
        {
            get { return _invicibilitytimer; }
        }

        public SplashKitSDK.Timer BlinkTimer
        {
            get { return _blinktimer; }
        }

        public int Damage
        {
            get { return _damage; }
        }
    }

}

using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Custom_project
{
    public class NightButterflyEnemy : Enemies
    {
        private float _angle = 0;
        private static SplashKitSDK.Timer _attacktimer;
        public NightButterflyEnemy():base("NightButterfly","the hurt butterfly", 20, 80, "butterfly")
        {
            _attacktimer = SplashKit.CreateTimer("butterflies");
            FireRate = 200;

            //Load in the Sprite and Bitmap
            Bitmap = SplashKit.LoadBitmap("butterflyenemy", "nightbutterfly.png");
            Sprite = SplashKit.CreateSprite("butterfly", Bitmap);

            Speed = 1;

            //Points for destroying the enemy
            Points = 120;
            _attacktimer.Start();
        }

        public override void Draw()
        {
            base.Draw();
            HealthBar.Draw(Sprite.X - 10,Sprite.Y-5);
        }

        public override void Update()
        {
            base.Update();
            Movement();
            if (_attacktimer.Ticks > FireRate)
            {
                ShootingBullets();
                _attacktimer.Reset();
            }          
        }

        public void Movement()
        {
            if (SplashKit.RectangleRight(Sprite.CollisionRectangle) >= Demo.window.Width)
            {
                MoveDirectionX = -1;
            }
            if (SplashKit.RectangleLeft(Sprite.CollisionRectangle) <= 0)
            {
                MoveDirectionX = 1;
            }
        }


        public override void ShootingBullets()
        {
            for (int i = 1; i <= 4; i++)
            {
                Bullets bullet = BulletPool.GetBullets();
                bullet.Sprite.X = this.Sprite.X;
                bullet.Sprite.Y = this.Sprite.Y;

                float bulDirX = MathF.Sin(((_angle + 270 * i) * MathF.PI) / 180F);
                float bulDirY = MathF.Cos(((_angle + 270 * i) * MathF.PI) / 180F);

                bullet.MoveDirectionX = bulDirX;
                bullet.MoveDirectionY = bulDirY;
                bullet.Speed = 3;

                Bullets.Add(bullet);
            }

            _angle += 10f;
            if (_angle >= 360f)
            {
                _angle = 0;
            }
        }
    }
}

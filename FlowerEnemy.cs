using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class FlowerEnemy : Enemies
    {
        private float _startangle = 125F, _endangle = 290F;
        public FlowerEnemy() : base("FlowerEnemy", "The fallen flower", 1, 20, "Flower")
        {
            //Load in the Sprite and Bitmap
            Bitmap = SplashKit.LoadBitmap("FlowerEnemy", "FlowerEnemy.png");
            Sprite = SplashKit.CreateSprite("Enemy", Bitmap);

            Points = 100;
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ShootingBullets()
        {
            int bulletammount = 3;
            float anglestep = (_endangle - _startangle) / bulletammount;
            float angle = _startangle;
            for (int i=0; i<bulletammount; i++)
            {
                Bullets bullet = BulletPool.GetBullets();
                bullet.Sprite.X = this.Sprite.X;
                bullet.Sprite.Y = this.Sprite.Y;

                float bulDirX = MathF.Sin((angle * MathF.PI) / 180F);
                float bulDirY = MathF.Cos((angle * MathF.PI) / 180F);

                bullet.MoveDirectionX = bulDirX;
                bullet.MoveDirectionY = -bulDirY;

                Bullets.Add(bullet);
                angle += anglestep;
            }
        }
    }
}

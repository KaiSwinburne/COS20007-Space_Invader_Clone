using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class TVCrabEnemy : Enemies
    {
        public TVCrabEnemy():base("TVCrabEnemy","The TV crab",1,20, "TVCrab")
        {
            //Load in the Sprite and Bitmap
            Bitmap = SplashKit.LoadBitmap("Enemy", "TVCrabEnemy.png");
            Sprite = SplashKit.CreateSprite("Enemy", Bitmap);

            //Points for destroying the enemy
            Points = 50;          
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
            Bullets bullet = BulletPool.GetBullets();
            bullet.Sprite.X = Sprite.X;
            bullet.Sprite.Y = Sprite.Y;
            bullet.MoveDirectionX = 0;
            bullet.MoveDirectionY = 1;
            Bullets.Add(bullet);
        }
    }
}

using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class EnemyBullets : Bullets
    {
        public EnemyBullets(string enemytype)
        {
            switch (enemytype)
            {
                case "TVCrab":
                    Bitmap = new Bitmap("TVCrabBullet", "TVCrabBullet.png");
                    break;
                case "Flower":
                    Bitmap = new Bitmap("Flowerbullet", "FlowerBullet.png");
                    break;
                case "butterfly":
                    Bitmap = new Bitmap("Butterfly", "Butterfly.png");
                    break;
            }
            
            Sprite = SplashKit.CreateSprite(Bitmap);
            Speed = 9F;
        }

        public override void Update()
        {
            SplashKit.UpdateSprite(Sprite);
            Sprite.Y += Speed * MoveDirectionY * Program.deltatime * Program.targetFPS;
            Sprite.X += Speed * MoveDirectionX * Program.deltatime * Program.targetFPS;
        }


    }
}

using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class PlayerBullets : Bullets
    {

        public PlayerBullets()
        {
            Bitmap = SplashKit.LoadBitmap("round","playerbullet.png");
            Sprite = SplashKit.CreateSprite(Bitmap);
            Speed = 15;
        }

        public override void Update()
        {       
            Sprite.Y -= Speed * Program.deltatime * Program.targetFPS;
        }
    }
}

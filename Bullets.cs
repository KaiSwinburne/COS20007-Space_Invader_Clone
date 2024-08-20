using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public abstract class Bullets : GameObjects
    {
        private float _speed; //how fast the bullet will travel
        private Vector2D _movedirection;
        public Bullets() :base("bullet","test bullet")
        {  
 
        }

        public void Draw()
        {
            SplashKit.DrawSprite(Sprite);      //draw the sprite            
        }
        public abstract void Update();
        public bool BulletOutOfScreen()
        {
            if (Sprite.X > Demo.window.Width+20 || Sprite.X < -20 || Sprite.Y > Demo.window.Height+20 || Sprite.Y < -20)
            {
                return true;
            }
            return false;
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public float MoveDirectionX
        {
            get { return Convert.ToSingle(_movedirection.X); }
            set { _movedirection.X = value; }
        }

        public float MoveDirectionY
        {
            get { return Convert.ToSingle(_movedirection.Y); }
            set { _movedirection.Y = value; }
        }
    }
}

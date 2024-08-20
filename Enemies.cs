using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Custom_project;
using SplashKitSDK;

namespace Custom_project
{
    public abstract class Enemies : Actor,IHavePoints
    {
        private float _movedirectionx;
        private float _movedirectiony;
        private BulletPool _bulletpool;
        private List<Bullets> _bullets;
        private int _points;

        public Enemies(string name, string description, double maxhp, int poolsize,string enemytype):base(name, description, maxhp)
        {
            _bullets = new List<Bullets>();
            _movedirectionx = 1;

            //create a new bullet pool for the enemy
            _bulletpool = new BulletPool(enemytype, poolsize);

            Speed = 2;
        }

        public override void Draw()
        {
            SplashKit.DrawSprite(Sprite);   //draw the sprite
            //SplashKit.DrawRectangle(Color.White, Sprite.CollisionRectangle);    //draw the collision rectangle                               
            foreach (Bullets bullet in _bullets)
            {
                bullet.Draw();
            }
        }

        public virtual void Update()
        {
            Sprite.X += Speed * _movedirectionx * Program.deltatime * Program.targetFPS;                       
            BulletsUpdate();           
        }

        public void MoveDown(int distance)
        {
            Sprite.Y += distance;
        }
        
        public void BulletsUpdate()
        {
            foreach (EnemyBullets bullet in _bullets)
            {
                bullet.Update();
            }
            
            for (int i=0; i<_bullets.Count; i++)
            {
                if (_bullets[i].BulletOutOfScreen())
                {
                    EnemyBullets recyclebullet = _bullets[i] as EnemyBullets;
                    _bulletpool.ReturnBullets(recyclebullet);
                    _bullets.Remove(_bullets[i]);
                    break;
                }
            }
        }

        public bool GetDamaged(Player player)
        {
            foreach (PlayerBullets playerbullet in player.Bullets)
            {
                if (SplashKit.RectanglesIntersect(playerbullet.Sprite.CollisionRectangle, this.Sprite.CollisionRectangle))
                {                   
                    player.BulletPool.ReturnBullets(playerbullet);
                    player.Bullets.Remove(playerbullet);           
                    return true;
                }             
            }
            return false;
        }       
        
        public List<Bullets> Bullets
        {
            get { return _bullets; }
        }

        public BulletPool BulletPool
        {
            get { return _bulletpool; }
        }

        public float MoveDirectionX
        {
            get { return _movedirectionx; }
            set { _movedirectionx = value; }
        }

        public float MoveDirectionY
        {
            get { return _movedirectiony; }
            set { _movedirectiony = value; }
        }

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }
    }
}

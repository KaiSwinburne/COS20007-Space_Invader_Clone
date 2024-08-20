using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public abstract class Actor : GameObjects
    {
        private float _speed;
        private HealthBar _healthbar;
        private Rectangle _damagecollision;
        private int _firerate; //the firing rate (ms)
        public Actor(string name, string description, double maxhp) :base(name,description)
        {
            _healthbar = new HealthBar(name,description,maxhp);
            _damagecollision = new Rectangle();
        }

        public abstract void Draw();

        public abstract void ShootingBullets();

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public HealthBar HealthBar
        {
            get { return _healthbar; }
        }

        public Rectangle DamageCollision
        {
            get { return _damagecollision; }
        }

        public int FireRate
        {
            get { return _firerate; }
            set { _firerate = value; }
        }
        public double DamageCollisionWidth
        {
            set { _damagecollision.Width = value; }
            get { return _damagecollision.Width; }
        }

        public double DamageCollisionHeight
        {
            get { return _damagecollision.Height; }
            set { _damagecollision.Height = value; }
        }

        public double DamageCollisionX
        {
            set { _damagecollision.X = value; }
            get { return _damagecollision.X; }
        }

        public double DamageCollisionY
        {
            set { _damagecollision.Y = value; }
            get { return _damagecollision.Y; }
        }
    }
}

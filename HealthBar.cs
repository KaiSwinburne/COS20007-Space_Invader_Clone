using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Custom_project;
using SplashKitSDK;

namespace Custom_project
{
    public class HealthBar : GameObjects
    {
        private double _health, _maxhealth;

        public HealthBar(string name, string description, double maxhp):base(name,description)
        {
            _maxhealth = maxhp;
            _health = _maxhealth;          
        }

        public void Draw(double x, double y)
        {
            if (_health < 0)
            {
                _health = 0;
            }
            SplashKit.FillRectangle(Color.Red, x, y,67, 2);
            SplashKit.FillRectangle(Color.White, x, y, 67*(_health/_maxhealth) , 2);
            
        }


        public double Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public double MaxHealth
        {
            get { return _maxhealth; }
            set { _maxhealth = value; }
        }
    }
}

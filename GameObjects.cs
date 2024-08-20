using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public abstract class GameObjects
    {
        private string _name;
        private string _description;
        private Bitmap _bitmap;
        private Sprite _sprite;
        
        public GameObjects(string name, string description)
        {
            _name = name;
            _description = description;
        }
        public GameObjects():this("demoplayer","test player")
        {

        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
    }
}

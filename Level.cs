using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public abstract class Level:ICanBeReset
    {
        private List<string> _id;
        private bool _selected;
        private bool _ispaused;
        public Level(string[] idstring)
        {
            _selected = false;
            _ispaused = false;
            _id = new List<string>();
            foreach (string id in idstring)
            {
                _id.Add(id.ToLower());
            }
        }

        public bool AreYou(string id)
        {
            return _id.Contains(id.ToLower());
        }
        
        public abstract void Update();
        public abstract void Draw();

        public abstract void Reset();


        public bool Selected
        {
            set { _selected = value; }
            get { return _selected; }
        }

        public bool IsPaused
        {
            get { return _ispaused; }
            set { _ispaused = value; }
        }

        public string LevelName
        {
            get { return _id[0]; }
        }
    }
}

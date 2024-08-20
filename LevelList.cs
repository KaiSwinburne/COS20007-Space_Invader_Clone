using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Custom_project
{
    public class LevelList
    {
        private List<Level> _levellist;
        public LevelList()
        {
            _levellist = new List<Level>();
        }

        public void AddLevel(Level level)
        {
            _levellist.Add(level);
        }

        public void RemoveLevel(string id)
        {
            foreach (Level level in _levellist)
            {
                if (level.AreYou(id))
                {
                    _levellist.Remove(level);
                }
            }            
        }

        public void LoadLevel(string id)
        {
            //if no levels need to be loaded
            if (id == "")
            {
                return;
            }
            foreach (Level level in _levellist)
            {
                if (level.AreYou(id))
                {
                    level.Selected = true;
                }
            }
        }

        public void UnloadLevel(string id)
        {
            //if no levels need to be unloaded
            //if no levels need to be loaded
            if (id == "")
            {
                return;
            }
            foreach (Level level in _levellist)
            {
                if (level.AreYou(id))
                {
                    level.Selected = false;
                }
            }
        }

        public void UnpauseLevel(string id)
        {
            //if no levels need to be loaded
            if (id == "")
            {
                return;
            }
            foreach (Level level in _levellist)
            {
                if (level.AreYou(id))
                {
                    level.IsPaused =false;
                }
            }
        }

        public void ResetLevel(string id)
        {
            //if no levels need to be loaded
            if (id == "")
            {
                return;
            }
            foreach (Level level in _levellist)
            {
                if (level.AreYou(id))
                {
                    level.Reset();
                }
            }
        }

        public Level ReturnLevel(string id)
        {
            foreach (Level level in _levellist)
            {
                if (level.AreYou(id))
                {
                    return level;
                }
            }
            return null;
        }

        public Level ReturnCurrentLevel()
        {
            foreach (Level level in _levellist)
            {
                if (level.AreYou("playable")&&level.Selected)
                {
                    return level;
                }
            }
            return null;
        }

        public void BeginCountdownTimer(IHaveCountdownBeforeStart levelcountdown)
        {
            levelcountdown.CountdownTimer.Start();
        }

        public void UnloadCurrentLevel()
        {
            foreach (Level level in _levellist)
            {
                if (level.Selected && level.LevelName != "pausemenu")
                {
                    level.Selected = false;
                }
            }
        }

        public void UnpauseCurrentLevel()
        {
            foreach (Level level in _levellist)
            {
                if (level.Selected && level.LevelName != "pausemenu")
                {
                    level.IsPaused = false;
                }
            }
        }

        public void ResetCurrentLevel()
        {
            foreach (Level level in _levellist)
            {
                if (level.Selected && level.LevelName != "pausemenu")
                {
                    level.Reset();
                }
            }
        }

        
        public void Update()
        {
            foreach (Level level in _levellist)
            {
                if (level.Selected)
                {
                    level.Update();
                }
            }
        }

        public void Draw()
        {
            foreach (Level level in _levellist)
            {
                if (level.Selected)
                {
                    level.Draw();                  
                }
            }
            
        }

        public List<Level> Listing
        {
            get { return _levellist; }
        }
    }
}

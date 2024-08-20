using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Custom_project
{
    public class EnemiesTable: ICanBeReset
    {
        private List<Enemies> _enemies;
        private static SplashKitSDK.Timer _attacktimer;
        private int _firerate, _rows, _columns;
        private float _direction, _columngap, _rowgap, _rectspeed, _intialspeed;
        private Rectangle _group;
        private double _speedincreaserate;

        public EnemiesTable()
        {
            _attacktimer = SplashKit.CreateTimer("Enemies' attack timer");
            _attacktimer.Start();
            _firerate = 300;
            _enemies = new List<Enemies>();
            _group = new Rectangle();
            _direction = 1;
            _rows = 5;
            _columns = 8;
            _columngap = 10.0f;
            _rowgap = 10.0f;
            _speedincreaserate = 18.0;
        }

        public void SpawnEnemiesTable()
        {
            float tablewidth = (47.0f+_columngap) * _columns;
            _group.Width = tablewidth;
            float tableheight = (32.0f+_rowgap) * (_rows);
            _group.Height = tableheight;
            
            for (int row=0; row<_rows; row++)
            {
                Vector2D rowposition = new Vector2D();
                rowposition.X = (Demo.window.Width - tablewidth)/2;      
                rowposition.Y += (Demo.window.Height - tableheight)/4 + (row * (32.0f+ _rowgap)); //divide more if the table is to be higher
                
                for (int column=0; column<_columns; column++)
                {
                    Vector2D columnposition = rowposition;
                    columnposition.X += column * (47.0f+_columngap);
                    Enemies enemy = new TVCrabEnemy(); 
                    if (row > 2)
                    {
                        enemy = new FlowerEnemy();
                    }             
                    enemy.Sprite.X = Convert.ToSingle(columnposition.X);
                    enemy.Sprite.Y = Convert.ToSingle(columnposition.Y);
                    _enemies.Add(enemy);
                }                
            }
            Enemies butterflyenemy = new NightButterflyEnemy();
            butterflyenemy.Sprite.X = Demo.window.Width/2;
            butterflyenemy.Sprite.Y = 45;
            _enemies.Add(butterflyenemy);

            _group.X = Enemies[0].Sprite.X-5;
            _group.Y = Enemies[0].Sprite.Y-5;
            _intialspeed = Enemies[0].Speed;
            _rectspeed = _intialspeed;
        }

        public void Draw()
        {
           // SplashKit.DrawRectangle(Color.Yellow, _group);
            foreach (Enemies enemy in Enemies)
            {
                if (enemy.HealthBar.Health > 0)
                {
                    enemy.Draw();
                }               
            }
        }

        public void Update(Player player)
        {           
            //this is for the table rectangle movement
            _group.X += _rectspeed*_direction * Program.deltatime * Program.targetFPS;

            foreach (Enemies enemy in _enemies)
            {
                enemy.Update();                                           
            }

            MovementSpeed();
            EnemiesMovement();

            //if timer exceeds firerate, the enemy attack
            if (_attacktimer.Ticks > _firerate)
            {
                EnemiesAttack();
                _attacktimer.Reset();
            }

            //this is to prevent code from leaping out of the loop, causing other sprite to not update
            EnemiesGetDamaged(player);
            RemoveEnemy(player);
        }

        public void EnemiesAttack()
        {
            int randomnumber = SplashKit.Rnd(0, _enemies.Count);
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (randomnumber == i)
                {
                    _enemies[i].ShootingBullets();
                }
            }         
        }

        public void EnemiesMovement()
        {
            if (SplashKit.RectangleRight(_group) >= Demo.window.Width)
            {
                _direction = -1;
                for (int i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].MoveDirectionX = -1;
                    _enemies[i].MoveDown(3);
                }
                _group.Y += 3;
            }
            if (SplashKit.RectangleLeft(_group) <= 0)
            {
                _direction = 1;
                for (int i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].MoveDirectionX = 1;
                    _enemies[i].MoveDown(3);
                }
                _group.Y += 3;
            }
        }

        public void Reset()
        {
            _enemies.Clear();
            SpawnEnemiesTable();
            _attacktimer.Reset();
            _direction = 1;
        }

        public void MovementSpeed()
        {
            if (_group.Y > Convert.ToDouble(Demo.window.Height * _speedincreaserate/100.0) && _rectspeed<4)
            {
                _rectspeed = Convert.ToSingle(_rectspeed * (1.0 + 20.0 / 100.0));
                for (int i = 0; i < _enemies.Count; i++)
                {
                    _enemies[i].Speed = _rectspeed;
                }
                _speedincreaserate = _speedincreaserate + 5;
            }          
        }

        public void EnemiesGetDamaged(Player player)
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].GetDamaged(player))
                {
                    _enemies[i].HealthBar.Health -= player.Damage;
                    break;
                }
            }
        }

        public void RemoveEnemy(Player player)
        {
            foreach (Enemies enemy in _enemies)
            {
                if (enemy.HealthBar.Health<0)
                {
                    _enemies.Remove(enemy);
                    player.Points += enemy.Points;
                    break;
                }
            }
        }
        public List<Enemies> Enemies
        {
            get { return _enemies; }
        }

        public SplashKitSDK.Timer AttackTimer
        {
            get { return _attacktimer; }
        }
    }
}

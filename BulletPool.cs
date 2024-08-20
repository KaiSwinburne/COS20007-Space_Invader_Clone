using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using Custom_project;

namespace Custom_project
{
    //The object pool purpose is to initializes the pool

    public class BulletPool
    {
        private Queue<Bullets> _pooledBullets = new Queue<Bullets>();
        private int _amountToPool;
        public BulletPool(string kind, int amount)
        {
            _amountToPool = amount;
            switch (kind)
            {
                case "player":
                    for (int i = 0; i < _amountToPool; i++)
                    {
                        Bullets bullet = new PlayerBullets();
                        _pooledBullets.Enqueue(bullet);                        
                    }
                    break;
                default:
                    for (int i = 0; i < _amountToPool; i++)
                    {
                        Bullets bullet = new EnemyBullets(kind);
                        _pooledBullets.Enqueue(bullet);                       
                    }
                    break;
            }          
        }

        public Bullets GetBullets()
        {           
            Bullets bullet = _pooledBullets.Dequeue();
            return bullet;
            
            /*
            switch (_pooltype)
            {
                default:
                    if (_pooledBullets.Count >= 0)
                    {
                        Bullets bullet = _pooledBullets.Dequeue();
                        return bullet; 
                    }
                    else
                    {
                        Bullets bullet = new PlayerBullets();
                        return bullet;
                    }                 
                case Kind.Enemy:
                    if (_pooledBullets.Count >= 0)
                    {
                        Bullets bullet = _pooledBullets.Dequeue();
                        return bullet;
                    }
                    else
                    {
                        Bullets bullet = new EnemyBullets();
                        return bullet;
                    }
            }
            */
            //if the pool has bullets, return the bullets           
        }

        public void ReturnBullets(Bullets bullet)
        {
            _pooledBullets.Enqueue(bullet);
        }

    }
}

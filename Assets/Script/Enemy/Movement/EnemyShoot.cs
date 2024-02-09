using Script.Behavior;
using UnityEngine;

namespace Script.Enemy.Movement
{
    public class EnemyShoot : ShootBehavior
    {
        public void ShootEnemy(float moveX, float moveY,bool shoot, int diff)
        {
            FireBullet(moveX,moveY,shoot,diff,false);
        }
        
    }
}
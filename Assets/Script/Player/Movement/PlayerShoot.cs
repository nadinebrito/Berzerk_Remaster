using Script.Behavior;
using UnityEngine;

namespace Script.Player.Movement
{
    public class PlayerShoot : ShootBehavior
    {
        private bool _shooting;
        private float _moveX;
        private float _moveY;
        private int _force =10;
        
        private void Update()
        {
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveY = Input.GetAxisRaw("Vertical");
            _shooting = Input.GetButton("Shoot");
        }

        private void FixedUpdate()
        {
            FireBullet(_moveX,_moveY,_shooting,_force);
        }
        
    }
}

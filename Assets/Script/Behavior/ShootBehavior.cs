using System;
using FMOD;
using UnityEngine;

namespace Script.Behavior
{
    public class ShootBehavior : MonoBehaviour
    {
        protected GameObject BulletGameObject;
        public GameObject bulletPrefab;
        protected Rigidbody2D BulletRb2d;
        protected Transform PlayerGameObject;
        private Vector2 _newDirection;
        
        private void Start()
        {
            BulletGameObject = Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            BulletGameObject.SetActive(false);
            BulletRb2d = BulletGameObject.GetComponent<Rigidbody2D>();
            PlayerGameObject = gameObject.transform.parent.gameObject.transform;
            
        }

        
        protected void FireBullet(float moveX, float moveY, bool shooting, int force , bool player)
        {
            
            if (!shooting || BulletGameObject.activeSelf) return;
            if (player)
            {
                
                FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerShoot");
            }

            if (!player)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyShoot");
            }

            
            BulletRotation(moveX,moveY);
            
            if (moveX == 0 && moveY == 0)
            {
                _newDirection = PlayerGameObject.rotation.y == 0 ? new Vector2(force, 0) : new Vector2(-force, 0);
            }
            else
            {
                _newDirection = new Vector2(force * moveX, force * moveY);
            }
            
            BulletGameObject.transform.position = gameObject.transform.position;
            BulletGameObject.SetActive(true);
            BulletRb2d.AddForce(_newDirection, ForceMode2D.Impulse);
        }
        
        private void BulletRotation(float moveX, float moveY)
        {
            switch (moveX)
            {
                case 0 when moveY == 0:
                case > 0 when moveY == 0:
                    BulletGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;
            }

            BulletGameObject.transform.rotation = moveX switch
            {
                > 0 when moveY > 0 => Quaternion.Euler(0, 0, 45),
                0 when moveY > 0 => Quaternion.Euler(0, 0, 90),
                < 0 when moveY > 0 => Quaternion.Euler(0, 0, 135),
                _ => BulletGameObject.transform.rotation
            };
            if (moveX < 0 && moveY == 0)
            {
                BulletGameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            }

            BulletGameObject.transform.rotation = moveX switch
            {
                < 0 when moveY < 0 => Quaternion.Euler(0, 0, 225),
                0 when moveY < 0 => Quaternion.Euler(0, 0, 270),
                > 0 when moveY < 0 => Quaternion.Euler(0, 0, 315),
                _ => BulletGameObject.transform.rotation
            };
        }
    }
}

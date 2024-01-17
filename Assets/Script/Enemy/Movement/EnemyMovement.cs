using System;
using System.Collections;
using Script.Behavior;
using Script.Game;
using UnityEngine;

namespace Script.Enemy.Movement
{
    public class EnemyMovement : MovementBehavior
    {
        private bool _isDead;


        public void MoveEnemy(float moveX, float moveY, int dif,bool shoot)
        {
            Move(moveX,moveY,dif,shoot, _isDead);
        }

        public void AnimaEnemy(float moveX, float moveY,bool shoot)
        {
            Anima(moveX, moveY, shoot, false,_isDead);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDead) return;
            if (other.CompareTag("Player") || other.CompareTag("BulletPlayer") || other.CompareTag("BulletEnemy")||other.CompareTag("Enemy") ||other.CompareTag("Wall")||other.CompareTag("Otto"))
            {
                Rb2d.bodyType = RigidbodyType2D.Static;
                _isDead = true;
                GameObject.Find("GameController").GetComponent<GameController>().EnemyDeathScore();
                StartCoroutine(Fade());
            }
        }
        
        private IEnumerator Fade()
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
        }
    }
}

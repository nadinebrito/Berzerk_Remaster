using Script.Behavior;
using Script.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using FixedUpdate = Unity.VisualScripting.FixedUpdate;

namespace Script.Player.Movement
{
    public class PlayerMovement : MovementBehavior
    {
        private float _moveX;
        private float _moveY;
        private bool _shooting;
        private bool _isDead;
        private GameObject _gameController;
        
        [SerializeField] private int speed;
        
        
        
        private void Update()
        {
            
            _moveX = Input.GetAxisRaw("Horizontal");
            _moveY = Input.GetAxisRaw("Vertical");
            _shooting = Input.GetButton("Shoot");
            Anima(_moveX,_moveY,_shooting,true,_isDead);
            GunPosition(_moveX,_moveY,_shooting);
        }

        private void FixedUpdate()
        {
            Move(_moveX,_moveY,speed,_shooting,_isDead);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.CompareTag("BulletEnemy") && !_isDead) || (other.CompareTag("Enemy")&& !_isDead) || ( other.CompareTag("Wall")&& !_isDead) || ( other.CompareTag("Otto")&& !_isDead))
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/PlayerDeath");
                GunSystem.SetActive(false);
                Rb2d.bodyType = RigidbodyType2D.Static;
                _isDead = true;
                _gameController = GameObject.Find("GameController");
                _gameController.GetComponent<GameController>().DeathOnRoom();
            };
            
        }

        public void Revive()
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            _isDead = false;
        }
    }
}

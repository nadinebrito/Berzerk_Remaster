using UnityEngine;

namespace Script.Enemy
{
    public class OttoBehavior : MonoBehaviour
    {

        public GameObject playerGameObject;
        private float velocidade = 0.5f;
        private Rigidbody2D _playerRb2d;

        private void Start()
        {
            _playerRb2d = playerGameObject.GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            gameObject.tag = "Otto";
            
        }

        public void DeathEnemyRage()
        {
            velocidade += 0.5f;
        }

        public void ResetVel()
        {
            velocidade = 0.5f;
        }

        private void FixedUpdate()
        {
        
            Vector3 direcao = playerGameObject.transform.position - transform.position;
            direcao.Normalize();
            transform.Translate(direcao * (velocidade * Time.deltaTime));
        }

        private void Update()
        {
            if(_playerRb2d.bodyType == RigidbodyType2D.Static && gameObject.CompareTag("Otto") )
            {
                gameObject.tag = "Untagged";
            }
        }
    }
}

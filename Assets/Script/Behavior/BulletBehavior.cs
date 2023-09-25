using UnityEngine;

namespace Script.Behavior
{
    public class BulletBehavior : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.CompareTag("BulletPlayer") || other.CompareTag("BulletEnemy"))
            {
                gameObject.SetActive(false);
            }
            
            if (gameObject.CompareTag("BulletPlayer"))
            {
                if (other.CompareTag("Enemy") || other.CompareTag("GoalArea") || other.CompareTag("Wall"))
                {
                    gameObject.SetActive(false);
                }
            }

            if (gameObject.CompareTag("BulletEnemy"))
            {
                if (other.CompareTag("Enemy") || other.CompareTag("Player") || other.CompareTag("Wall"))
                {
                    gameObject.SetActive(false);
                }
                
            }
        }
    }
}

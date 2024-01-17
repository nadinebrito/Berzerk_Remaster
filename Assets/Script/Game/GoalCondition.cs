using UnityEngine;

namespace Script.Game
{
    public class GoalCondition : MonoBehaviour
    {
        private GameController _gameController;
        private void Awake()
        {
            _gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _gameController.NextRoom(gameObject.name);
            }
        }
    }
}

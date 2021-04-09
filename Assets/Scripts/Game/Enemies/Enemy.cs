using Channels;
using Game.ScriptableObjects;
using Levels;
using UnityEngine;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private LevelChannel _levelChannel;
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private EnemyData _enemyData;

        private float _currentHealth;

        Collider2D _enemyCollider2D;

        private void Awake()
        {
            _currentHealth = _enemyData.Health;
            _enemyCollider2D = GetComponent<Collider2D>();
            _levelChannel.levelCompletedEvent += LevelCompletedEvent;
        }

        private void LevelCompletedEvent(Level arg0, Level arg1)
        {
            Die();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Component shot;
            Component interactable;

            if (other.TryGetComponent(typeof(Shot), out shot))
            {
                _currentHealth -= 50;

                if (_currentHealth <= 0)
                {
                    Die();
                }
                
                Destroy(shot.gameObject);
            }
            
            if(other.TryGetComponent(typeof(IEnemyInteractable), out interactable))
            {
                _enemyCollider2D.enabled = false;
                (interactable as IEnemyInteractable)?.EnemyCollided(this);
            }
        }

        public void Die()
        {
            Destroy(this.gameObject);
            _gameChannel.OnEnemyDestroyed(this);
        }
    }
}

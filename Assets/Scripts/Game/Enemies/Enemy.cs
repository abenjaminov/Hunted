using Channels;
using Game.ScriptableObjects;
using UnityEngine;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;
        [SerializeField] private EnemyData _enemyData;

        private float _currentHealth;

        Collider2D _enemyCollider2D;

        private void Awake()
        {
            this._currentHealth = _enemyData.Health;
            _enemyCollider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Component shot;
            Component interactable;

            if (other.TryGetComponent(typeof(Shot), out shot))
            {
                this._currentHealth -= 50;

                if (this._currentHealth <= 0)
                {
                    this.Die();
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

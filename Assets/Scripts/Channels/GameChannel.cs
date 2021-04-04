using Game;
using Game.Enemies;
using JetBrains.Annotations;
using Snake;
using UnityEngine;
using UnityEngine.Events;

namespace Channels
{
    [CreateAssetMenu(fileName = "GameChannel", menuName = "Channels/Game Channel", order = 0)]
    public class GameChannel : ScriptableObject
    {
        public UnityAction<Collectable> collectedEvent;
        public UnityAction<Collectable> collectableDesctroyedEvent;
        public UnityAction<SnakeBodyPart> bodyPartDestroyedEvent;
        public UnityAction<SnakeBodyPart> bodyPartCreatedEvent;
        public UnityAction<Enemy> enemyDestroyedEvent;

        public void OnCollected(Collectable collectable)
        {
            collectedEvent?.Invoke(collectable);
        }

        public void OnCollectableDestroyed(Collectable collectable)
        {
            collectableDesctroyedEvent?.Invoke(collectable);
        }

        public void OnBodyPartDestroyed(SnakeBodyPart destroyedPart)
        {
            bodyPartDestroyedEvent?.Invoke(destroyedPart);
        }

        public void OnBodyPartCreated(SnakeBodyPart createdPart)
        {
            bodyPartCreatedEvent?.Invoke(createdPart);
        }
        
        public void OnEnemyDestroyed(Enemy destroyedEnemy)
        {
            enemyDestroyedEvent?.Invoke(destroyedEnemy);
        }
    }
}
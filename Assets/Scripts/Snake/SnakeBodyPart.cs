using System;
using System.Collections;
using System.Collections.Generic;
using Channels;
using Game;
using Game.Enemies;
using UnityEngine;

namespace Snake
{
    public class SnakeBodyPart : MonoBehaviour, IEnemyInteractable
    {
        [SerializeField] protected GameChannel _gameChannel;
        [SerializeField] protected InputChannel _inputChannel;
        [SerializeField] protected MovementChannel _movementChannel;
        protected SnakeBodyPartMovement _snakeBodyPartMovement;

        private float _delayTime = 0;

        private bool _isFirstChild;

        protected Vector3 MovementDirection;

        protected virtual void Start()
        {
            _snakeBodyPartMovement = GetComponent<SnakeBodyPartMovement>();
        }

        private void MoveEvent(Vector2 input)
        {
            StartCoroutine(MoveDelayed(input));
        }

        private IEnumerator MoveDelayed(Vector2 input)
        {
            yield return new WaitForSeconds(_delayTime);
            _snakeBodyPartMovement.Move(input);
            MovementDirection = _snakeBodyPartMovement.MovementDirection;

            if (ChildPart != null)
            {
                ChildPart.MoveEvent(input);
            }
        }

        public SnakeBodyPart ParentPart { get; private set; }
        public SnakeBodyPart ChildPart { get; private set; }
        
        public void SetParent(SnakeBodyPart parentPart, float delayTimeS, bool isFirstChild = false)
        {
            _isFirstChild = isFirstChild;
            
            if (isFirstChild)
            {
                _inputChannel.moveEvent += MoveEvent;
            }
            
            ParentPart = parentPart;
            _delayTime = delayTimeS;
            
            MoveEvent(ParentPart.MovementDirection);
        }
        
        public void SetChild(SnakeBodyPart childPart)
        {
            ChildPart = childPart;
        }

        public virtual void EnemyCollided(Enemy enemy)
        {
            print(enemy.name + " Just collided with me");
            Destroy(enemy.gameObject);
            Destroy(gameObject);
            _inputChannel.moveEvent -= MoveEvent;

            if (ChildPart != null)
            {
                ChildPart.SetParent(ParentPart, _delayTime + ChildPart._delayTime, _isFirstChild);
                ParentPart.SetChild(ChildPart);
            } 
            
            _gameChannel.OnBodyPartDestroyed(this);
        }
    }
}

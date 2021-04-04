using System;
using Channels;
using Game;
using UnityEngine;

namespace Snake
{
    public class Snake : MonoBehaviour, IGrowPowerupAcceptor
    {
        [SerializeField] private GameObject snakeBodyPart;
        private SnakeHead _snakeHead;

        private SnakeBodyPart _previousPart;

        [SerializeField] private GameChannel _gameChannel;

        private void Awake()
        {
            _snakeHead = GetComponent<SnakeHead>();
            this._gameChannel.bodyPartDestroyedEvent += BodyPartDestroyed;
        }

        private void BodyPartDestroyed(SnakeBodyPart destroyedPart)
        {
            if (destroyedPart == _previousPart)
            {
                print("Last Destroyed " + destroyedPart);
                 _previousPart = _snakeHead == destroyedPart.ParentPart ? null : destroyedPart.ParentPart;
            }
        }

        public void Grow()
        {
            GameObject obj = Instantiate(snakeBodyPart, 
                _previousPart == null ? transform.position: _previousPart.transform.position, 
                Quaternion.identity);
            
            var bodyPart = obj.GetComponent<SnakeBodyPart>();
            if (_previousPart == null)
            {
                bodyPart.SetParent(_snakeHead, .35f, true);
                _snakeHead.SetChild(bodyPart);
            }
            else
            {
                _previousPart.SetChild(bodyPart);
                bodyPart.SetParent(_previousPart, .25f);
            }
                
            // We Invoke the event here since we dont want the event to invoke
            // for the snake head
            _gameChannel.OnBodyPartCreated(bodyPart);
            
            _previousPart = bodyPart;
        }
    }
}

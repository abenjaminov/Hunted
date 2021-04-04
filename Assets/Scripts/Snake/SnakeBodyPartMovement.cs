using System;
using Channels;
using Game;
using UnityEngine;

namespace Snake
{
    public class SnakeBodyPartMovement : MonoBehaviour, IOutOfBoundsSubscriber, ISpeedBoostPowerUpAcceptor
    {
        [SerializeField] private float speed = 3f;
        [HideInInspector] public Vector3 MovementDirection;

        void Start()
        {
            MovementDirection = Vector3.zero;
        }

        public void Move(Vector2 direction)
        {
            if (direction.x != 0 && Math.Abs((direction.x * -1) - MovementDirection.x) > float.Epsilon)
            {
                MovementDirection = new Vector3(direction.x, 0, 0);
            }
            else if (direction.y != 0 && Math.Abs((direction.y * -1) - MovementDirection.y) > float.Epsilon)
            {
                MovementDirection = new Vector3(0, direction.y, 0);
            }
        }

        void Update()
        {
            transform.position += MovementDirection * (speed * Time.deltaTime);
        }

        public void OnOutOfBounds(Bounds bounds)
        {
            Vector3 newPosition = Vector3.zero;
            
            if (transform.position.x > bounds.max.x)
            {
                newPosition = new Vector3(bounds.min.x, transform.position.y, 0);
            }
            else if (transform.position.x < bounds.min.x)
            {
                newPosition = new Vector3(bounds.max.x, transform.position.y, 0);
            }
            else if (transform.position.y > bounds.max.y)
            {
                newPosition = new Vector3(transform.position.x, bounds.min.y, 0);
            }
            else if (transform.position.y < bounds.min.y)
            {
                newPosition = new Vector3(transform.position.x, bounds.max.y, 0);
            }

            transform.position = newPosition;
        }

        public void BoostSpeed(float factor)
        {
            this.speed *= factor;
        }
    }
}

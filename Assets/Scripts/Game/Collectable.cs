using System;
using Channels;
using UnityEngine;

namespace Game
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private GameChannel _gameChannel;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            Component shot;

            if (other.TryGetComponent(typeof(Shot), out shot))
            {
                _gameChannel.OnCollectableDestroyed(this);
                Destroy(gameObject);
            }
        }
    }
}

using System;
using UnityEngine;

namespace Game
{
    public class GameBounds : MonoBehaviour
    {
        private Bounds _bounds;
        
        private void Awake()
        {
            var collider2D = GetComponent<Collider2D>();
            _bounds = collider2D.bounds;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Component outOfBoundsSubscriber;

            if (other.TryGetComponent(typeof(IOutOfBoundsSubscriber), out outOfBoundsSubscriber))
            {
                (outOfBoundsSubscriber as IOutOfBoundsSubscriber)?.OnOutOfBounds(_bounds);
            }
        }
    }
}

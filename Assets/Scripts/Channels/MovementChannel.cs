using UnityEngine;
using UnityEngine.Events;

namespace Channels
{
    [CreateAssetMenu(fileName = "MovementChannel", menuName = "Channels/Movement Channel", order = 0)]
    public class MovementChannel : ScriptableObject
    {
        public UnityAction<Vector3> movementDirectionEvent;
        public UnityAction<Vector3> snakePositionbChanged; // position

        public void OnMovementDirectionChanged(Vector3 movementDirection)
        {
            movementDirectionEvent?.Invoke(movementDirection);
        }

        public void OnPositionChanged(Vector3 position)
        {
            snakePositionbChanged?.Invoke(position);
        }
    }
}
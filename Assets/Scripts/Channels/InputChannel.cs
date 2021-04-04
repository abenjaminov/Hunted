using UnityEngine;
using UnityEngine.Events;

namespace Channels
{
    [CreateAssetMenu(fileName = "InputChannel", menuName = "Channels/Input Channel", order = 0)]
    public class InputChannel : ScriptableObject
    {
        public UnityAction<Vector2> moveEvent;

        public void OnMoveEvent(Vector2 directionNormalized)
        {
            moveEvent?.Invoke(directionNormalized);
        }
    }
}
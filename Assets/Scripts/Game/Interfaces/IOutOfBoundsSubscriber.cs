using UnityEngine;

namespace Game
{
    public interface IOutOfBoundsSubscriber
    {
        void OnOutOfBounds(Bounds bounds);
    }
}
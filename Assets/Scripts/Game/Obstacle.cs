using Game.ScriptableObjects;
using UnityEngine;

namespace Game
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private PathFindingData _pathFindingData;

        private SpriteRenderer _renderer;
        
        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            var bounds = _renderer.bounds;
            _pathFindingData.SetNonWalkableArea(bounds.center, bounds.size.x, bounds.size.y);        
        }
    }
}

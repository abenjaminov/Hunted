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
            _pathFindingData.SetNonWalkableArea(transform.position - new Vector3(bounds.size.x / 2, bounds.size.y / 2), bounds.size.x, bounds.size.y);        
        }
    }
}

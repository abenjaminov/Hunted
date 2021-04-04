using System;
using System.Collections.Generic;
using Game.ScriptableObjects;
using UnityEngine;
using Utils;

namespace Game.Enemies
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private PathFindingData pathFindingData;

        [HideInInspector] private GameObject _objectToFollow;
        [SerializeField] private float timeBetweenPulses;
        [SerializeField] private float _speed;
        
        private float _timeUntillNextPulse;

        private List<Vector2> _movementPositions;

        private int nextPositionIndex = 0;
        
        private void Awake()
        {
            _movementPositions = new List<Vector2>();
        }

        private void Update()
        {
            _timeUntillNextPulse -= Time.deltaTime;

            if (_timeUntillNextPulse <= 0)
            {
                _timeUntillNextPulse = timeBetweenPulses;   
                CreatePath();
            }

            if (_movementPositions.Count <= 0) return;

            var nextPosition = _movementPositions[nextPositionIndex];
            transform.position = 
                Vector3.MoveTowards(transform.position, nextPosition, _speed * Time.deltaTime);
        }

        private void CreatePath()
        {
            var followPosition = _objectToFollow.transform.position;
            
            _movementPositions = pathFindingData.FindPath(transform.position,followPosition);

            _movementPositions.Add(followPosition.To2D());
        }
        
        public void FollowObject(GameObject objectToFollow)
        {
            _objectToFollow = objectToFollow;
        }
    }
}